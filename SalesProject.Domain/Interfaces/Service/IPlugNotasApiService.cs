using SalesProject.Domain.Entities;
using System.Collections.Generic;

namespace SalesProject.Domain.Interfaces.Service
{
    public interface IPlugNotasApiService
    {
        string SendInvoice(Invoice invoice);
        string SendAllInvoices(ICollection<Invoice> invoices);
        string ConsultSefaz(string invoiceIdPlugNotas, ref bool hasDoneWithSuccess);
        string DownloadInvoicePdf(string invoiceIdPlugNotas);
        string DownloadInvoiceXml(string invoiceIdPlugNotas);
    }
}