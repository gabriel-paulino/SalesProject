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

        public const string
            Id = "Id gerado pela aplicação.",
            PostingDate = "Data de Lançamento do Pedido.",
            DeliveryDate = "Data de Entrega do Pedido.",
            Status = "Status do Pedido. 1:Aberto, 2:Aprovado, 3:Faturado, 4:Cancelado.",
            TotalOrder = "Total do Pedido.",
            Observation = "Observações para o Pedido.",
            CustomerId = "Vínculo com a tabela de Cliente (Fk).";
    }
}