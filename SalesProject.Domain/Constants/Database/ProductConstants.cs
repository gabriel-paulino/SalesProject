namespace SalesProject.Domain.Constants.Database
{
    public struct ProductConstants
    {
        public const string
            TableProduct = "Produto",
            FieldName = "Nome",
            FieldNcmCode = "CodigoNcm",
            FieldCombinedPrice = "PrecoCombinado",
            FieldAdditionalCosts = "CustosAdicionais",
            FieldCombinedQuantity = "PrevisaoMensal",
            FieldDetails = "Detalhes",
            FieldCustomerId = "ClienteId";

        public const string
            Id = "Id gerado pela aplicação.",
            Name = "Nome do Produto.",
            NcmCode = "Código NCM do Produto.",
            CombinedPrice = "Preço combinado do Produto.",
            AdditionalCosts = "Custos adicionais combinado para o Produto.",
            CombinedQuantity = "Previsão mensal mínima combinada para o Produto.",
            Details = "Detalhes do Produto.",
            CustomerId = "Vínculo com a tabela de Cliente (Fk).";
    }
}