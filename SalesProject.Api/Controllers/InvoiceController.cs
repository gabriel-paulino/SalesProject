using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesProject.Domain.Interfaces.Repository;
using SalesProject.Domain.Services;
using System;
using System.Linq;
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

            if (invoice is not null)
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

            if (id is not null)
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

            if (invoiceIdPlugNotas is not null)
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

            if (order is null)
                return BadRequest($"Ops. Não foi possível criar a Nota Fiscal. Id do Pedido de venda é inválido.");

            if (!order.CanBillThisOrder(order.Status))
                return BadRequest($"Ops. Apenas pedidos aprovados podem ser faturados.");

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

            if (invoice.IsIntegrated())
                return BadRequest($"Ops. Essa Nota Fiscal já foi enviada.");

            string errorMessage = _plugNotasApiService.SendInvoice(invoice);

            if (string.IsNullOrEmpty(errorMessage))
                return Ok(@$"Nota Fiscal com Id '{invoice.Id}' enviada com sucesso.");

            return BadRequest(errorMessage);
        }

        /// <summary>
        /// Sends All non-integrated Invoices to the Plug Notas Api.
        /// </summary>
        /// <returns></returns>
        [HttpPatch]
        [Authorize(Roles = "Administrator")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Route("api/[controller]/send-all")]
        public IActionResult SendAll()
        {
            var invoices = _invoiceService.GetAllInvoicesAbleToSend();

            if (!invoices.Any())
                return BadRequest($"Ops. Não existem Notas fiscais a serem enviadas.");

            string errorMessage = _plugNotasApiService.SendAllInvoices(invoices);

            if (string.IsNullOrEmpty(errorMessage))
                return Ok(@$"Todas as {invoices.Count} Notas Fiscais foram enviadas com sucesso.");

            return BadRequest(errorMessage);
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
            bool hasDoneWithSuccess = false;
            string content = _plugNotasApiService.ConsultSefaz(invoiceIdPlugNotas, ref hasDoneWithSuccess);

            if (hasDoneWithSuccess)
                return Ok(content);

            return BadRequest(content);
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
            string errorMessage = _plugNotasApiService.DownloadInvoicePdf(invoiceIdPlugNotas);

            if (string.IsNullOrEmpty(errorMessage))
                return Ok($"Download do Pdf da Nota fiscal realizado com sucesso. Pdf disponível em 'C:/nota-fiscal/pdf/{invoiceIdPlugNotas}.pdf'");

            return BadRequest(errorMessage);
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
            string errorMessage = _plugNotasApiService.DownloadInvoiceXml(invoiceIdPlugNotas);

            if (string.IsNullOrEmpty(errorMessage))
                return Ok($"Download do Xml da Nota fiscal realizado com sucesso. Xml disponível em 'C:/nota-fiscal/xml/{invoiceIdPlugNotas}.xml'");

            return BadRequest(errorMessage);
        }
    }
}