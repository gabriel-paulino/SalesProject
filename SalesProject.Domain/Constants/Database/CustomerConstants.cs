namespace SalesProject.Domain.Constants.Database
{
    public struct CustomerConstants
    {
        public const string
            TableClient = "Cliente",
            FieldId = "Id",
            FieldCnpj = "Cnpj",
            FieldCompanyName = "Empresa",
            FieldOpening = "DataAbertura",
            FieldTelNumber = "Telefone",
            FieldClientSince = "DataCadastro",
            FieldMunicipalRegistration = "InscricaoMunicipal",
            FieldStateRegistration = "InscricaoEstadual",
            FieldEmail = "Email";

        public const string
            Id = "Id gerado pela aplicação.",
            Cnpj = "Número do cadastro nacional de pessoa jurídica do cliente.",
            CompanyName = "Nome da empresa do cliente.",
            Email = "E-mail da empresa do cliente",
            StateRegistration = "Inscrição estadual da empresa do cliente.",
            Opening = "Data de abertura da empresa do cliente.",
            Phone = "Telefone da empresa do cliente.",
            ClientSince = "Data de cadastrado do cliente no sistema.",
            MunicipalRegistration = "Inscrição municipal da empresa do cliente.";
    }
}