namespace SalesProject.Domain.Constants.Database
{
    public struct InvoiceConstants
    {
        public const string
            TableInvoice = "NotaFiscal",
            FieldOriginOperation = "NaturezaOperacao",
            FieldReleaseDate = "DataEmissao",
            FieldBaseCalcIcms = "BaseCalculoIcms",
            FieldTotalIcms = "ValorIcms",
            FieldTotalProducts = "TotalProdutos",
            FieldTotalInvoice = "TotaNota",
            FieldIntegratedPlugNotasApi = "NotaEnviada",
            FieldOrderId = "PedidoId";
    }
}