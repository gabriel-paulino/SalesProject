namespace SalesProject.Domain.Constants.Database
{
    public struct SalesOrderConstants
    {
        public const string
            TableSalesOrder = "PedidoVenda",
            FieldPostingDate = "DataLancamento",
            FieldDeliveryDate = "DataEntrega",
            FieldFreight = "Frete",
            FieldTotalTax = "TotalImposto",
            FieldTotalDiscount = "TotalDesconto",
            FieldTotalPriceProducts = "TotalProdutos",
            FieldTotalOrder = "TotalPedido",
            FieldObservation = "Observacao";
    }
}