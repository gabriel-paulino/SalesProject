namespace SalesProject.Domain.Constants.Database
{
    public struct OrderLinesConstants
    {
        public const string
            TableSalesOrderLines = "ItemPedidoVenda",
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
            FieldIpiAliquot = "AliqutaIpi";
    }
}