namespace SalesProject.Domain.Constants.Database
{
    public struct OrderConstants
    {
        public const string
            TableOrder = "Pedido",
            FieldPostingDate = "DataLancamento",
            FieldDeliveryDate = "DataEntrega",
            FieldStatus = "Status",
            FieldTotalOrder = "Total",
            FieldObservation = "Observacao",
            FieldCustomerId = "ClienteId";
    }
}