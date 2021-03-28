namespace SalesProject.Domain.Constants.Database
{
    public struct OrderConstants
    {
        public const string
            TableOrder = "PedidoVenda",
            FieldPostingDate = "DataLancamento",
            FieldDeliveryDate = "DataEntrega",
            FieldFreight = "Frete",
            FieldTotalTax = "TotalImposto",
            FieldTotalDiscount = "TotalDesconto",
            FieldTotalPriceProducts = "TotalProdutos",
            FieldTotalOrder = "TotalPedido",
            FieldObservation = "Observacao",
            FieldCustomerId = "ClienteId";
    }
}