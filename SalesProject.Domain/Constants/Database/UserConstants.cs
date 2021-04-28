namespace SalesProject.Domain.Constants.Database
{
    public struct UserConstants
    {
        public const string
            TableUser = "Usuario",
            FieldUsername = "Usuario",
            FieldEmail = "Email",
            FieldPasswordHash = "SenhaCriptografada",
            FieldRole = "Funcao",
            FieldCustomerId = "ClienteId";
    }
}