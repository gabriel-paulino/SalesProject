using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using SalesProject.Domain.Enums;
using SalesProject.Domain.Interfaces.Repository;
using SalesProject.Domain.Services;
using System;
using System.Linq;
using System.Net;
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
            var order = _orderRepository.GetToCreateInvoice(orderId);

            if(order is null)
                return BadRequest($"Ops. Não foi possível criar a Nota Fiscal. Id do Pedido de venda é inválido.");

            if(order.Status != OrderStatus.Approved)
                return BadRequest($"Ops. Apenas pedidos aprovados podem ser faturados.");

            if (!order.Customer.Adresses.Where(a => a.Type == AddressType.Billing).Any())
                return BadRequest($"Ops. O cliente '{order.Customer.CompanyName}' não possuí um endereço de cobranca cadastrado.");

            var invoice = _invoiceService.CreateBasedInOrder(order);

            if (!invoice.Valid)
                return ValidationProblem($"{invoice.GetNotification()}");

            return Created(
            $@"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}/{invoice.Id}",
            invoice);
        }

        /// <summary>
        /// Send an Invoice to Plug Notas Api.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch]
        [Authorize(Roles = "Administrator")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Route("api/[controller]/send/{id:guid}")]
        public IActionResult Send(Guid id)
        {
            var invoice = _invoiceService.Get(id);

            if (invoice is null)
                return BadRequest($"Ops. Não foi possível recuperar Nota Fiscal com Id: '{id}'.");

            if (invoice.IntegratedPlugNotasApi == 'Y')
                return BadRequest($"Ops. Essa Nota fiscal já foi enviada.");

            var response = (IRestResponse)_plugNotasApiService.SendInvoice(invoice);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                //Atualizar coluna BD marcar que foi integrado a NF
                _invoiceService.MarkAsIntegrated(invoice);

                return Ok(response.Content);
            }
             
            return BadRequest(response.Content);
        }

    }
}
