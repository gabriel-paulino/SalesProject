﻿namespace SalesProject.Domain.Constants.Database
{
    public struct OrderLinesConstants
    {
        public const string
            TableOrderLines = "ItemPedidoVenda",
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
            FieldOrderId = "PedidoVendaId",
            FieldProductId = "ProdutoId";
    }
}