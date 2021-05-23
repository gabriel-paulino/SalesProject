namespace SalesProject.Domain.Constants.Database
{
    public struct InvoiceLinesConstants
    {
        public const string
            TableInvoiceLines = "ItemNotaFiscal",
            FieldItemName = "NomeProduto",
            FieldNcmCode = "CodigoNcm",
            FieldQuantity = "Quantidade",
            FieldOriginOperation = "NaturezaOperacao",
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
            FieldAliquotPis = "AliquotaPis",
            FieldValuePis = "ValorPis",
            FieldCstCofins = "CstCofins",
            FieldValueBaseCalcCofins = "ValorBaseCalcCofins",
            FieldAliquotCofins = "AliquotaCofins",
            FieldValueCofins = "ValorCofins";
    }
}