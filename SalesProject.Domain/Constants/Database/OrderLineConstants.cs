namespace SalesProject.Domain.Constants.Database
{
    public struct OrderLineConstants
    {
        public const string
            TableOrderLines = "ItemPedido",
            FieldQuantity = "Quantidade",
            FieldUnitaryPrice = "ValorUnitario",
            FieldTotalPrice = "TotalItem",
            FieldAdditionalCosts = "CustosAdicionais",
            FieldOrderId = "PedidoId",
            FieldProductId = "ProdutoId";

        public const string
            Id = "Id gerado pela aplicação.",
            Quantity = "Quantidade do item do Pedido.",
            UnitaryPrice = "Valor unitário do item do Pedido.",
            TotalPrice = "Total do item do Pedido.",
            AdditionalCosts = "Custos adicionais do item do Pedido.",
            OrderId = "Vínculo com a tabela de Pedido (Fk).",
            ProductId = "Vínculo com a tabela de Produto (Fk).";
    }
}