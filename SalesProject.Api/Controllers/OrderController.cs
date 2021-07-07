using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesProject.Api.ViewModels.Order;
using SalesProject.Domain.Entities;
using SalesProject.Domain.Enums;
using SalesProject.Domain.Interfaces;
using SalesProject.Domain.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;

namespace SalesProject.Api.Controllers
{
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly IOrderRepository _orderRepository;        
        private readonly IUnitOfWork _uow;

        public OrderController(
            ICustomerRepository customerRepository,
            IProductRepository productRepository,
            IUserRepository userRepository,
            IOrderRepository orderRepository,
            IUnitOfWork uow)
        {
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
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
            var order = _orderRepository.Get(id);

            if (order is not null)
                return Ok(order);

            return NotFound($"Ops. Pedido de venda com Id:'{id}' não foi encontrado.");
        }

        /// <summary>
        /// Get all Orders using a filter.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Customer,Seller,Administrator")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Route("api/[controller]/filter")]
        public IActionResult GetOrderUsingFilter([FromQuery] OrderFilterViewModel model)
        {
            var filter = model.Status is not null
                ? new OrderFilter(
                    customerId: model.CustomerId,
                    status: (OrderStatus)model.Status,
                    startDate: model.StartDate,
                    endDate: model.EndDate
                    )
                : new OrderFilter(
                    customerId: model.CustomerId,
                    startDate: model.StartDate,
                    endDate: model.EndDate
                    );

            if (!filter.Valid)
                return ValidationProblem($"{filter.GetNotification()}");

            var orders = _orderRepository.GetOrdersUsingFilter(filter);

            if (orders.Count != 0)
                return Ok(orders);

            return NoContent();
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

            if (isCustomer)
            {
                string username = User.Identity.Name;
                var user = _userRepository.GetByUsername(username);

                if(user.CustomerId != Guid.Parse(model.CustomerId))
                    return ValidationProblem(
                        $"Ops. Não é possível criar esse pedido. " +
                        $"O usuário '{username}' apenas pode criar pedidos para cliente '{_customerRepository.Get(Guid.Parse(user.CustomerId.ToString())).CompanyName}'.");
            }

            var customer = _customerRepository.GetCustomerWithAdressesAndContacts(Guid.Parse(model.CustomerId));

            if(customer is null)
                return BadRequest($"Ops. Não foi possível criar o Pedido de Venda. Cliente não existe.");

            var order =
                    new Order(
                        postingDate: DateTime.Parse(model.PostingDate).Date,
                        deliveryDate: DateTime.Parse(model.DeliveryDate).Date,
                        observation: model.Observation,
                        customer: customer
                        );

            var customerErrors = new List<string>();

            foreach (var line in model.OrderLines)
            {
                var product = _productRepository.Get(Guid.Parse(line.ProductId));

                if (product is null)
                    return ValidationProblem($"Ops. Produto com Id:'{line.ProductId}' não foi encontrado.");

                var orderLine =
                    new OrderLine(
                                   quantity: line.Quantity,
                                   unitaryPrice: line.UnitaryPrice,
                                   additionalCosts: line.AdditionalCosts,
                                   product: product
                                   );

                if(orderLine.Product.CustomerId != order.Customer.Id)
                    return ValidationProblem(
                        $"Ops. Não é possível criar esse pedido. " +
                        $"O produto '{orderLine.Product.Name}' não pertence ao cliente '{order.Customer.CompanyName}'");

                if (isCustomer)
                {
                    if (product.CombinedQuantity != orderLine.Quantity)
                        customerErrors.Add($"O produto '{orderLine.Product.Name}' deve possuir quantidade igual à '{product.CombinedQuantity}'.");

                    if (product.CombinedPrice != orderLine.UnitaryPrice)
                        customerErrors.Add($"O produto '{orderLine.Product.Name}' deve possuir preço unitário igual à '{product.CombinedPrice.ToString("C2")}'.");

                    if (product.AdditionalCosts != orderLine.AdditionalCosts)
                        customerErrors.Add($"O produto '{orderLine.Product.Name}' deve possuir custos adicionais igual à '{product.AdditionalCosts.ToString("C2")}'.");
                }

                order.AddOrderLine(orderLine);
            }

            if (isCustomer && customerErrors.Any())
            {
                string errorMessage = string.Empty;

                foreach (var error in customerErrors)
                    errorMessage = $"{errorMessage} " +
                                   $"{error}";

                errorMessage = $"Ops... Algo deu errado: " +
                    $"{errorMessage} " +
                    $"Conforme definido no contrato.";
                
                return ValidationProblem(errorMessage);
            }

            if (!order.Valid)
                return ValidationProblem($"{order.GetNotification()}");

            _orderRepository.Create(order);
            _uow.Commit();

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