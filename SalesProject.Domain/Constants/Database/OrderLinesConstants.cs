namespace SalesProject.Domain.Constants.Database
{
    public struct OrderLinesConstants
    {
        public const string
            TableOrderLines = "ItemPedidoVenda",
            FieldQuantity = "Quantidade",
            FieldUnitaryPrice = "PrecoUnitario",
            FieldTotalPrice = "PrecoTotal",
            FieldAdditionalCosts = "CustosAdicionais",
            FieldOrderId = "PedidoVendaId",
            FieldProductId = "ProdutoId";
    }
}