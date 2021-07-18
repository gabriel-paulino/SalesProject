using Newtonsoft.Json;
using SalesProject.Domain.Dtos;
using SalesProject.Domain.Extensions;
using SalesProject.Domain.Interfaces.Service;
using System.IO;
using System.Net;

namespace SalesProject.Application.Services
{
    public class AddressApiService : IAddressApiService
    {
        public AddressApi CompleteAddressApi(string zipCode)
        {
            zipCode = zipCode.CleanZipCode();

            var url = $"https://viacep.com.br/ws/{zipCode}/json/";
            string json = GetJsonResponse(url);

            var addressResponse = !string.IsNullOrEmpty(json)
                ? JsonConvert.DeserializeObject<AddressApi>(json)
                : null;

            return addressResponse is not null
                ? new AddressApi(
                    cep: addressResponse.Cep,
                    logradouro: addressResponse.Logradouro,
                    bairro: addressResponse.Bairro,
                    localidade: addressResponse.Localidade,
                    uf: addressResponse.Uf,
                    ibge: addressResponse.Ibge
                    )
                : null;
        }

        private static string GetJsonResponse(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.AutomaticDecompression = DecompressionMethods.GZip;

                string json = string.Empty;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    json = reader.ReadToEnd();
                }

                return json;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}