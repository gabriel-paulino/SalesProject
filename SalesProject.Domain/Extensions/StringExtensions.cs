namespace SalesProject.Domain.Extensions
{
    public static class StringExtensions
    {
        public static string CleanZipCode(this string zipCode)
        {
            zipCode = zipCode.Replace("-", string.Empty);

            return zipCode.Length == 7
                ? $"0{zipCode}"
                : zipCode;
        }

        public static string CleanCnpj(this string cnpj) =>
            cnpj.Replace(".", string.Empty)
                .Replace("%2F", string.Empty)
                .Replace("-", string.Empty);
    }
}