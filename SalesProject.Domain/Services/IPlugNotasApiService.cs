using SalesProject.Domain.Entities;

namespace SalesProject.Domain.Services
{
    public interface IPlugNotasApiService
    {
        object SendInvoice(Invoice invoice);
        object ConsultSefaz(string invoiceIdPlugNotas);
        object DownloadInvoicePdf(string invoiceIdPlugNotas);
        object DownloadInvoiceXml(string invoiceIdPlugNotas);
    }
}