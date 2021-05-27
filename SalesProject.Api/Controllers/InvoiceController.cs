using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using SalesProject.Domain.Dtos;
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
        /// Get Invoice by orderId.
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize()]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("api/[controller]/{orderId:guid}")]
        public IActionResult GetByOrderId(Guid orderId)
        {
            var invoice = _invoiceService.GetByOrderId(orderId);

            if (invoice != null)
                return Ok(invoice);

            return NotFound($"Ops. Nenhuma Nota fiscal vinculada ao Pedido: '{orderId}' foi encontrada.");
        }

        /// <summary>
        /// Get only InvoiceId of a specific Order.
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize()]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("api/[controller]/id/{orderId:guid}")]
        public IActionResult GetIdByOrderId(Guid orderId)
        {
            var id = _invoiceService.GetInvoiceIdByOrderId(orderId);

            if (id != null)
                return Ok(id);

            return NotFound($"Ops. Nenhum Id de Nota fiscal vinculado ao Pedido: '{orderId}' foi encontrado.");
        }

        /// <summary>
        /// Get only Id of Invoice of Plug Notas Api by a specific Order.
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize()]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("api/[controller]/plug-notas-id/{orderId:guid}")]
        public IActionResult GetInvoiceIdOfPlugNotasByOrderId(Guid orderId)
        {
            var invoiceIdPlugNotas = _invoiceService.GetInvoiceIdOfPlugNotasByOrderId(orderId);

            if (invoiceIdPlugNotas != null)
                return Ok(invoiceIdPlugNotas);

            return NotFound($"Ops. Nenhum Id de Nota fiscal  vinculado ao Pedido: '{orderId}' foi encontrado.");
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
        [Authorize(Roles = "Seller,Administrator")]
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
                var plugNotasResponse = JsonConvert.DeserializeObject<PlugNotasResponse>(response.Content);

                string invoiceIdPlugNotas = string.Empty;

                foreach (var document in plugNotasResponse.Documents)
                {
                    invoiceIdPlugNotas = document.Id;
                    break;
                }

                _invoiceService.MarkAsIntegrated(invoice, invoiceIdPlugNotas);
                return Ok(response.Content);
            }             
            return BadRequest(response.Content);
        }

        /// <summary>
        /// Get information about an Invoice in Sefaz using the Id of Plug Notas Api.
        /// </summary>
        /// <param name="invoiceIdPlugNotas"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Seller,Administrator")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Route("api/[controller]/consult/{invoiceIdPlugNotas}")]
        public IActionResult Consult(string invoiceIdPlugNotas)
        {
            var response = (IRestResponse)_plugNotasApiService.ConsultSefaz(invoiceIdPlugNotas);

            if (response.StatusCode == HttpStatusCode.OK)
                return Ok(response.Content);
            
            return BadRequest(response.Content);
        }

        /// <summary>
        /// Downaload a PDF of an Invoice by Id of Plug Notas Api.
        /// </summary>
        /// <param name="invoiceIdPlugNotas"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize()]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Route("api/[controller]/download-pdf/{invoiceIdPlugNotas}")]
        public IActionResult DownloadPdf(string invoiceIdPlugNotas)
        {
            string response = (string)_plugNotasApiService.DownloadInvoicePdf(invoiceIdPlugNotas);

            if (string.IsNullOrEmpty(response))
                return Ok($"Download do Pdf da Nota fiscal realizado com sucesso. Pdf disponível em 'C:/nota-fiscal/pdf/{invoiceIdPlugNotas}.pdf'");

            return BadRequest(response);
        }

        /// <summary>
        /// Download a XML of an Invoice by Id of Plug Notas Api.
        /// </summary>
        /// <param name="invoiceIdPlugNotas"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize()]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Route("api/[controller]/download-xml/{invoiceIdPlugNotas}")]
        public IActionResult DownloadXml(string invoiceIdPlugNotas)
        {
            string response = (string)_plugNotasApiService.DownloadInvoiceXml(invoiceIdPlugNotas);

            if (string.IsNullOrEmpty(response))
                return Ok($"Download do Xml da Nota fiscal realizado com sucesso. Xml disponível em 'C:/nota-fiscal/xml/{invoiceIdPlugNotas}.xml'");

            return BadRequest(response);
        }
    }
}