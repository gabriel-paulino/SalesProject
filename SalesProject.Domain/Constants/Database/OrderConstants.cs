namespace SalesProject.Domain.Constants.Database
{
    public struct OrderConstants
    {
        public const string
            TableOrder = "PedidoVenda",
            FieldPostingDate = "DataLancamento",
            FieldDeliveryDate = "DataEntrega",
            FieldTotalOrder = "TotalPedido",
            FieldObservation = "Observacao",
            FieldCustomerId = "ClienteId";
    }
}