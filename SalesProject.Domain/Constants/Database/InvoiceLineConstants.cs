namespace SalesProject.Domain.Constants.Database
{
    public struct InvoiceLineConstants
    {
        public const string
            TableInvoiceLines = "ItemNotaFiscal",
            FieldItemName = "NomeProduto",
            FieldNcmCode = "CodigoNcm",
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
            FieldAliquotPis = "AliquotaPis",
            FieldValuePis = "ValorPis",
            FieldCstCofins = "CstCofins",
            FieldValueBaseCalcCofins = "ValorBaseCalcCofins",
            FieldAliquotCofins = "AliquotaCofins",
            FieldValueCofins = "ValorCofins";

        public const string
            Id = "Id gerado pela aplicação.",
            ItemName = "Nome do produto do item da Nota fiscal.",
            NcmCode = "Código Ncm do item da Nota fiscal.",
            Quantity = "Quantidade do item da Nota fiscal.",
            UnitaryPrice = "Valor Unitário do item da Nota fiscal.",
            TotalPrice = "Valor total do item da Nota fiscal.",
            TotalTax = "Imposto total do item da Nota fiscal.",
            AdditionalCosts = "Custos adicionais do item da Nota fiscal.",
            InvoiceId = "Vínculo com a tabela de Nota fiscal (Fk).",
            OriginIcms = "Origem Icms do item da Nota fiscal.",
            CstIcms = "Cst Icms do item da Nota fiscal.",
            DeterminationMode = "Modalidade Determinacao do item da Nota fiscal.",
            ValueBaseCalcIcms = "Base Cálculo do Icms do item da Nota fiscal.",
            AliquotIcms = "Alíquota do Icms do item da Nota fiscal.",
            ValueIcms = "Valor do Icms do item da Nota fiscal.",
            CstPis = "Cst Pis do item da Nota fiscal.",
            ValueBaseCalcPis = "Base Cálculo do Pis do item da Nota fiscal.",
            AliquotPis = "Alíquota do Pis do item da Nota fiscal.",
            ValuePis = "Valor do Pis do item da Nota fiscal.",
            CstCofins = "Cst Cofins do item da Nota fiscal.",
            ValueBaseCalcCofins = "Valor Base Cálculo do Cofins do item da Nota fiscal.",
            AliquotCofins = "Alíquota do Cofins do item da Nota fiscal.",
            ValueCofins = "Valor do Cofins do item da Nota fiscal.";
    }
}