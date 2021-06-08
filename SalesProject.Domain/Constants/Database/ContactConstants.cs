namespace SalesProject.Domain.Constants.Database
{
    public struct ContactConstants
    {
        public const string
            TableContact = "Contato",
            FieldFirstName = "Nome",
            FieldLastName = "Sobrenome",
            FieldFullName = "NomeSobrenome",
            FieldEmail = "Email",
            FieldWhatsApp = "WhatsApp",
            FieldPhone = "Telefone",
            FieldCustomerId = "ClienteId";

        public const string
            Id = "Id gerado pela aplicação.",
            FirstName = "Primeiro nome do contato.",
            LastName = "Último nome do contato.",
            FullName = "Primeiro nome e último nome",
            Email = "E-mail do contato.",
            WhatsApp = "Número do WhatsApp do contato.",
            Phone = "Telefone do contato.",
            CustomerId = "Vínculo com a tabela de Cliente (Fk).";
    }
}