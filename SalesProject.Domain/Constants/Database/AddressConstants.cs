namespace SalesProject.Domain.Constants.Database
{
    public struct AddressConstants
    {
        public const string
            TableAddress = "Endereco",
            FieldDescription = "Descricao",
            FieldZipCode = "Cep",
            FieldType = "Tipo",
            FieldStreet = "Logradouro",
            FieldNeighborhood = "Bairro",
            FieldNumber = "Numero",
            FieldCity = "Cidade",
            FieldState = "Uf",
            FieldCodeCity = "CodigoIbgeCidade",
            FieldCustomerId = "ClienteId";

        public const string
            Id = "Id gerado pela aplicação.",
            Description = "Descrição do endereço.",
            ZipCode = "Código postal do endereço.",
            Type = "Tipo do endereço. 1: Cobrança, 2: Entrega, 3: Outro.",
            Street = "Logradouro do endereço.",
            Neighborhood = "Bairro do endereço.",
            Number = "Número do endereço.",
            City = "Cidade do endereço.",
            State = "Estado do endereço.",
            CodeCity = "Código IBGE da cidade. Usado para emissão de NF.",
            CustomerId = "Vínculo com a tabela de Cliente (Fk).";
    }
}