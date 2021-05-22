namespace SalesProject.Domain.Constants.Database
{
    public struct InvoiceLinesConstants
    {
        public const string
            TableInvoiceLines = "ItemNotaFiscal",
            FieldQuantity = "Quantidade",
            FieldUnitaryPrice = "ValorUnitario",
            FieldTotalPrice = "ValorTotal",
            FieldTotalTax = "ImpostoTotal",
            FieldAdditionalCosts = "CustosAdicionais",
            FieldInvoiceId = "NotaFiscalId",
            FieldOriginIcms = "OrigemIcms",
            FieldCstIcms = "CstIcms",
            FieldDeterminationMode = "ModalidadeDeterminacao",
            FieldValueBaseCalcIcms = "BaseCalcIcms",
            FieldAliquotIcms = "AliquotaIcms",
            FieldValueIcms = "ValorIcms",
            FieldCstPis = "CstPis",
            FieldValueBaseCalcPis = "BaseCalcPis",
            FieldQuantityBaseCalcPis = "QuantidadeBaseCalcPis",
            FieldAliquotPis = "AliquotaPis",
            FieldValuePis = "ValorPis",
            FieldCstCofins = "CstCofins",
            FieldValueBaseCalcCofins = "ValorBaseCalcCofins",
            FieldAliquotCofins = "AliquotaCofins",
            FieldValueCofins = "ValorCofins";
    }
}