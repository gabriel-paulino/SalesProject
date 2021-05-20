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
    }
}