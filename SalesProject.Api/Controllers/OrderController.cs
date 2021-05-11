using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesProject.Api.ViewModels.Order;
using SalesProject.Domain.Entities;
using SalesProject.Domain.Enums;
using SalesProject.Domain.Interfaces;
using SalesProject.Domain.Interfaces.Repository;
using System;
using System.Net.Mime;

namespace SalesProject.Api.Controllers
{
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _uow;

        public OrderController(
            ICustomerRepository customerRepository,
            IProductRepository productRepository,
            IOrderRepository orderRepository,
            IUnitOfWork uow)
        {
            _customerRepository = customerRepository;
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
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("api/[controller]/{id:guid}")]
        public IActionResult GetOrder(Guid id)
        {
            var order = _orderRepository.Get(id);

            if (order != null)
                return Ok(order);

            return NotFound($"Ops. Pedido de venda com Id:'{id}' não foi encontrado.");
        }

        /// <summary>
        /// Get all Orders using a filter.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("api/[controller]/filter")]
        public IActionResult GetOrderUsingFilter([FromQuery] OrderFilterViewModel model)
        {
            var filter = model.Status != null
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
                return ValidationProblem(detail: $"{filter.GetNotification()}");

            var orders = _orderRepository.GetOrdersUsingFilter(filter);

            if (orders.Count != 0)
                return Ok(orders);

            return NotFound($"Ops. Nenhum pedido foi encontrado, usando esse filtro.");
        }

        /// <summary>
        /// Create a Order.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("api/[controller]")]
        public IActionResult CreateOrder(CreateOrderViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var order =
                    new Order(
                        postingDate: DateTime.Parse(model.PostingDate).Date,
                        deliveryDate: DateTime.Parse(model.DeliveryDate).Date,
                        observation: model.Observation,
                        customer: _customerRepository.Get(Guid.Parse(model.CustomerId))
                        );

            foreach (var line in model.OrderItens)
            {
                var orderLine =
                    new OrderLines(
                                   quantity: line.Quantity,
                                   unitaryPrice: line.UnitaryPrice,
                                   additionalCosts: line.AdditionalCosts,
                                   product: _productRepository.Get(Guid.Parse(line.ProductId))
                                   );

                order.AddOrderLine(orderLine);
            }

            if (!order.Valid)
                return ValidationProblem(detail: $"{order.GetNotification()}");

            _orderRepository.Create(order);
            _uow.Commit();

            return Created(
            $@"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}/{order.Id}",
            order);
        }
    }
}