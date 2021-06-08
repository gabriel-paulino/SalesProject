using SalesProject.Domain.Entities;
using System.Collections.Generic;

namespace SalesProject.Domain.Services
{
    public interface IPlugNotasApiService
    {
        object SendInvoice(Invoice invoice);
        string SendAllInvoices(List<Invoice> invoices);
        object ConsultSefaz(string invoiceIdPlugNotas);
        string DownloadInvoicePdf(string invoiceIdPlugNotas);
        string DownloadInvoiceXml(string invoiceIdPlugNotas);
    }
}