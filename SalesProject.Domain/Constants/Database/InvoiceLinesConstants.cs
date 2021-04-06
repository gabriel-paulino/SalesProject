namespace SalesProject.Domain.Constants.Database
{
    public struct InvoiceLinesConstants
    {
        public const string
            TableInvoiceLines = "ItemNotaFiscal",
            FieldQuantity = "Quantidade",
            FieldUnitaryPrice = "PrecoUnitario",
            FieldTotalPrice = "PrecoTotal",
            FieldPercentageUnitaryDiscont = "PercentualDescontoUnitario",
            FieldValueUnitaryDiscont = "DescontoUnitario",
            FieldAdditionalCosts = "CustosAdicionais",
            FieldCst = "Cst",
            FieldCfop = "Cfop",
            FieldBaseCalcIcms = "BaseCalculoIcms",
            FieldIcmsValue = "ValorIcms",
            FieldIpiValue = "ValorIpi",
            FieldIcmsAliquot = "AliquotaIcms",
            FieldIpiAliquot = "AliquotaIpi",
            FieldInvoiceId = "NotaFiscalId";
    }
}