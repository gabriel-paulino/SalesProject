namespace SalesProject.Domain.Constants.Database
{
    public struct UserConstants
    {
        public const string
            TableUser = "Usuario",
            FieldUsername = "Usuario",
            FieldName = "Nome",
            FieldEmail = "Email",
            FieldPasswordHash = "SenhaCriptografada",
            FieldRole = "Funcao",
            FieldCustomerId = "ClienteId";

        public const string
            Id = "Id gerado pela aplicação.",
            Username = "Nome de usuário para realizar login no sistema.",
            Name = "Nome do usuário.",
            Email = "E-mail do usuário",
            PasswordHash = "Senha criptografada do usuário",
            Role = "Função do usuário. Atribui as permissões do usuário no sistema. 1:Cliente, 2:Vendedor, 3:Funcinário de TI, 4:Gestor.",
            CustomerId = "Vínculo com a tabela de Cliente (Fk).";
    }
}