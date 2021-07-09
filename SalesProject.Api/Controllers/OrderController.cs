using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesProject.Api.ViewModels.Order;
using SalesProject.Domain.Enums;
using SalesProject.Domain.Interfaces.Service;
using System;
using System.Linq;
using System.Net.Mime;

namespace SalesProject.Api.Controllers
{
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(
            IOrderService orderService)
        {
            _orderService = orderService;
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
            var order = _orderService.Cancel(id);

            if (order is null)
                return NotFound($"Ops. Pedido de venda com Id: '{id}' não foi encontrado.");

            if (!order.Valid)
                return ValidationProblem(order.GetNotification());

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
            var order = _orderService.Approve(id);

            if (order is null)
                return NotFound($"Ops. Pedido de venda com Id: '{id}' não foi encontrado.");

            if (!order.Valid)
                return ValidationProblem(order.GetNotification());

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

            var updatedOrder = _orderService.Edit(id, model);

            if (updatedOrder is null)
                return NotFound($"Ops. Pedido de Venda com Id: '{id}' não foi encontrado.");

            if (!updatedOrder.Valid)
                return ValidationProblem(updatedOrder.GetAllNotifications());

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

            var orderDashboard = _orderService.GetInformationByPeriod(model.StartDate, model.EndDate);

            if (orderDashboard is not null)
                return orderDashboard.Valid
                    ? Ok(orderDashboard)
                    : ValidationProblem(orderDashboard.GetAllNotifications());

            return NotFound($"Ops. Não foi possível encontrar dados para esse período.");
        }

    }
}