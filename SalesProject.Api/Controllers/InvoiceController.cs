using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesProject.Domain.Enums;
using SalesProject.Domain.Interfaces.Repository;
using SalesProject.Domain.Services;
using System;
using System.Net.Mime;

namespace SalesProject.Api.Controllers
{
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IPlugNotasApiService _plugNotasApiService;
        private readonly IInvoiceService _invoiceService;
        private readonly IOrderRepository _orderRepository;

        public InvoiceController(
            IPlugNotasApiService plugNotasApiService, 
            IInvoiceService invoiceService, 
            IOrderRepository orderRepository)
        {
            _plugNotasApiService = plugNotasApiService;
            _invoiceService = invoiceService;
            _orderRepository = orderRepository;
        }

        /// <summary>
        /// Create an Invoice based in an order.
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Seller,Administrator")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Route("api/[controller]/{orderId:guid}")]
        public IActionResult CreateInvoice(Guid orderId)
        {
            var order = _orderRepository.Get(orderId);

            if(order is null)
                return BadRequest($"Ops. Não foi possível criar a Nota Fiscal. Id do Pedido de venda é inválido.");

            if(order.Status != OrderStatus.Approved)
                return BadRequest($"Ops. Apenas pedidos aprovados podem ser faturados.");

            var invoice = _invoiceService.CreateBasedInOrder(order);

            if (!invoice.Valid)
                return ValidationProblem($"{invoice.GetNotification()}");

            return Created(
            $@"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}/{invoice.Id}",
            invoice);
        }
    }
}
