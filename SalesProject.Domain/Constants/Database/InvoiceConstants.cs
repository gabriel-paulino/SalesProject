namespace SalesProject.Domain.Constants.Database
{
    public struct InvoiceConstants
    {
        public const string
            TableInvoice = "NotaFiscal",
            FieldNumber = "Numero",
            FieldSeries = "Serie",
            FieldOriginOperation = "NaturezaOperacao",
            FieldReleaseDate = "DataEmissao",
            FieldLeaveDate = "DataSaida",
            FieldLeaveHour = "HoraSaida",
            FieldCarrierName = "RazaoSocialTransportadora",
            FieldPaidBy = "FretePorConta",
            FieldAnttCode = "CodigoAnnt",
            FieldLicensePlate = "PlacaVeiculo",
            FieldCarrierCnpj = "CnpjTransportadora",
            FieldStateRegistration = "IETransportadora",
            FieldQuantity = "Quantidade",
            FieldType = "Especie",
            FieldBranch = "Marca",
            FieldNumeration = "Numeracao",
            FieldGrossWeight = "PesoBruto",
            FieldNetWeight = "PesoLiquido",
            FieldCarrierAddress = "EnderecoTransportadora",
            FieldCarrierCity = "MunicipioTransportadora",
            FieldCarrierState = "UfTransportadora",
            FieldBaseCalcIcms = "BaseCalculoIcms",
            FieldTotalIcms = "ValorIcms",
            FieldBaseCalcOtherIcms = "BaseCalculoIcmsSubst",
            FieldTotalOtherIcms = "ValorIcmsSubst",
            FieldTotalProducts = "ValorTotalProdutos",
            FieldShippingCost = "ValorFrete",
            FieldInsuranceCost = "ValorSeguro",
            FieldTotalDiscont = "Desconto",
            FieldOtherCosts = "OutrasDespesas",
            FieldIpiValue = "ValorIpi",
            FieldTotalInvoice = "ValorTotaNotaFiscal",
            FieldAdditionalInformation = "DadosAdicionais",
            FieldReservedFisco = "ReservadoFisco",
            FieldCustomerId = "ClienteId",
            FieldOrderId = "PedidoVendaId";
    }
}