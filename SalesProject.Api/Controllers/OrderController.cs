using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesProject.Api.ViewModels.Order;
using SalesProject.Domain.Entities;
using SalesProject.Domain.Enums;
using SalesProject.Domain.Interfaces;
using SalesProject.Domain.Interfaces.Repository;
using SalesProject.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;

namespace SalesProject.Api.Controllers
{
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _uow;

        public OrderController(
            IOrderService orderService,
            IProductRepository productRepository,
            IOrderRepository orderRepository,
            IUnitOfWork uow)
        {
            _orderService = orderService;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _uow = uow;
        }

        /// <summary>
        /// Get Order by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize()]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("api/[controller]/{id:guid}")]
        public IActionResult GetOrder(Guid id)
        {
            var order = _orderService.Get(id);

            if (order is not null)
                return Ok(order);

            return NotFound($"Ops. Pedido de venda com Id: '{id}' não foi encontrado.");
        }

        /// <summary>
        /// Get all Orders using a filter.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Seller,Administrator")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("api/[controller]/filter")]
        public IActionResult GetOrderUsingFilter([FromQuery] OrderFilterViewModel model)
        {
            var filter = _orderService.GetFilterToDashBoard(model);

            if (!filter.Valid)
                return ValidationProblem(filter.GetNotification());

            var orders = _orderService.GetOrdersUsingFilter(filter);

            if (orders.Any())
                return Ok(orders);

            return NotFound();
        }

        /// <summary>
        /// Create an Order.
        /// Customers can create Orders, but they must follow the contract.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize()]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Route("api/[controller]")]
        public IActionResult CreateOrder(CreateOrderViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool isCustomer = User.IsInRole(RoleType.Customer.ToString());
            string username = User.Identity.Name;

            var order = _orderService.Create(model, isCustomer, username);

            if (!order.Valid)
                return ValidationProblem(order.GetAllNotifications());

            return Created(
            $@"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}/{order.Id}",
            order);
        }

        /// <summary>
        /// Cancel an Order.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch]
        [Authorize(Roles = "Seller,Administrator")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("api/[controller]/cancel/{id:guid}")]
        public IActionResult CancelOrder(Guid id)
        {
            var order = _orderRepository.Get(id);

            if (order is null)
                return NotFound($"Ops. Pedido de venda com Id:'{id}' não foi encontrado.");

            order.Cancel();

            if (!order.Valid)
                return ValidationProblem($"{order.GetNotification()}");

            _orderRepository.Update(order);
            _uow.Commit();

            return Ok(order);
        }

        /// <summary>
        /// Approve an Order.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch]
        [Authorize(Roles = "Seller,Administrator")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("api/[controller]/approve/{id:guid}")]
        public IActionResult ApproveOrder(Guid id)
        {
            var order = _orderRepository.Get(id);

            if (order is null)
                return NotFound($"Ops. Pedido de venda com Id:'{id}' não foi encontrado.");

            order.Approve();

            if (!order.Valid)
                return ValidationProblem($"{order.GetNotification()}");

            _orderRepository.Update(order);
            _uow.Commit();

            return Ok(order);
        }

        /// <summary>
        /// Update an Order.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch]
        [Authorize(Roles = "Seller,Administrator")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("api/[controller]/{id:guid}")]
        public IActionResult EditOrder(Guid id, EditOrderViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var currentOrder = _orderRepository.Get(id);

            if (currentOrder is null)
                return NotFound($"Ops. Pedido de Venda com Id:'{id}' não foi encontrado.");

            if (currentOrder.Status != OrderStatus.Open)
                return ValidationProblem($"Ops. Não é possível alterar esse Pedido, pois o Status não é 'Aberto'.");

            var updatedOrder = currentOrder.
                        Edit(
                        postingDate: DateTime.Parse(model.PostingDate).Date,
                        deliveryDate: DateTime.Parse(model.DeliveryDate).Date,
                        observation: model.Observation);

            if (!updatedOrder.Valid)
                return ValidationProblem($"{updatedOrder.GetNotification()}");

            List<OrderLine> newOrderLines = new List<OrderLine>();
            List<OrderLine> removedOrderLines = new List<OrderLine>();

            foreach (var orderLine in updatedOrder.OrderLines)
            {
                bool remove = false;

                for (int i = 0; i < model.OrderLines.Count; i++)
                {
                    Guid currentIdOrderLines = string.IsNullOrEmpty(model.OrderLines[i].Id)
                    ? new Guid()
                    : Guid.Parse(model.OrderLines[i].Id);

                    if (orderLine.Id == currentIdOrderLines)
                        break;

                    if (i == model.OrderLines.Count - 1)
                    {
                        remove = true;
                        break;
                    }
                }
                if (remove)
                    removedOrderLines.Add(orderLine);
            }

            foreach (var orderLineModel in model.OrderLines)
            {
                if (string.IsNullOrEmpty(orderLineModel.Id))
                {
                    var product = _productRepository.Get(Guid.Parse(orderLineModel.ProductId));

                    if (product is null)
                        return ValidationProblem($"Ops. Produto com Id:'{orderLineModel.ProductId}' não foi encontrado.");

                    var newOrderLine =
                            new OrderLine(
                                   quantity: orderLineModel.Quantity,
                                   unitaryPrice: orderLineModel.UnitaryPrice,
                                   additionalCosts: orderLineModel.AdditionalCosts,
                                   product: product
                                   );

                    if (!newOrderLine.Valid)
                        return ValidationProblem($"{newOrderLine.GetNotification()}");

                    newOrderLines.Add(newOrderLine);
                }
            }

            foreach (var orderLineModel in model.OrderLines)
            {
                if (string.IsNullOrEmpty(orderLineModel.Id))
                    continue;

                var orderLine =
                    updatedOrder
                    .OrderLines
                    .Where(ol => ol.Id == Guid.Parse(orderLineModel.Id))
                    .FirstOrDefault();

                var product = _productRepository.Get(Guid.Parse(orderLineModel.ProductId));

                if (product is null)
                    return ValidationProblem($"Ops. Produto com Id:'{orderLineModel.ProductId}' não foi encontrado.");

                orderLine.Edit(
                                quantity: orderLineModel.Quantity,
                                unitaryPrice: orderLineModel.UnitaryPrice,
                                additionalCosts: orderLineModel.AdditionalCosts,
                                product: product
                                );

                if (!orderLine.Valid)
                    return ValidationProblem($"{orderLine.GetNotification()}");
            }

            if (newOrderLines.Any())
                foreach (var newOrderLine in newOrderLines)
                    updatedOrder.AddOrderLine(newOrderLine);

            if (removedOrderLines.Any())
                foreach (var orderLine in removedOrderLines)
                    updatedOrder.RemoveOrderLine(orderLine);

            updatedOrder.UpdateTotalOrder();

            _orderRepository.Update(updatedOrder);
            _uow.Commit();

            return Ok(updatedOrder);
        }

        /// <summary>
        /// Get Order Information by period.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("api/[controller]/dashboard")]
        public IActionResult Dashboard([FromQuery] OrderDashboardViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var orderDashboard = _orderRepository.GetInformationByPeriod(model.StartDate, model.EndDate);

            if (orderDashboard is not null)
                return orderDashboard.Valid
                    ? Ok(orderDashboard)
                    : ValidationProblem($"{orderDashboard.GetNotification()}");

            return BadRequest($"Ops. Não foi possível encontrar dados para esse período pedido.");
        }

    }
}