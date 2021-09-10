namespace SalesProject.Domain.Extensions
{
    public static class StringExtension
    {
        public static string CleanZipCode(this string zipCode)
        {
            zipCode = zipCode.Replace("-", string.Empty);

            return zipCode.Length == 7
                ? $"0{zipCode}"
                : zipCode;
        }

        public static string CleanCnpjToUseOnUrl(this string cnpj) =>
            cnpj.Replace(".", string.Empty)
                .Replace("%2F", string.Empty)
                .Replace("-", string.Empty);

        public static string CleanCnpjToSaveDataBase(this string cnpj) =>
            cnpj.Replace(".", string.Empty)
                .Replace("/", string.Empty)
                .Replace("-", string.Empty);
    }
}