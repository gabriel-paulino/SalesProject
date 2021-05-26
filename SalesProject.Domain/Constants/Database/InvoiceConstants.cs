namespace SalesProject.Domain.Constants.Database
{
    public struct InvoiceConstants
    {
        public const string
            TableInvoice = "NotaFiscal",
            FieldOriginOperation = "NaturezaOperacao",
            FieldReleaseDate = "DataEmissao",
            FieldBaseCalcIcms = "BaseCalculoIcms",
            FieldTotalIcms = "ValorIcms",
            FieldTotalProducts = "TotalProdutos",
            FieldTotalInvoice = "TotalNota",
            FieldIntegratedPlugNotasApi = "NotaEnviada",
            FieldOrderId = "PedidoId";

        public const string
            Id = "Id gerado pela aplicação.",
            OriginOperation = "Natureza de Operação da Nota fiscal.",
            ReleaseDate = "Data de Emissão da Nota fiscal.",
            BaseCalcIcms = "Base para Calculo do Icms.",
            TotalIcms = "Valor total do Icms na Nota fiscal.",
            TotalProducts = "Valor total dos produtos da Nota fiscal.",
            TotalInvoice = "Valor total da Nota fiscal.",
            IntegratedPlugNotasApi = "Nota fiscal foi enviada para Sefaz? Y: Sim, N: Não.",
            OrderId = "Vínculo com a tabela de Pedido (Fk).";
    }
}