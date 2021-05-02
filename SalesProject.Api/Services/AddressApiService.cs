using Newtonsoft.Json;
using SalesProject.Domain.Entities;
using SalesProject.Domain.Services;
using System.IO;
using System.Net;

namespace SalesProject.Api.Services
{
    public class AddressApiService : IAddressApiService
    {
        public AddressApi CompleteAddressApi(string zipCode)
        {
            zipCode = zipCode.Replace("-", string.Empty);

            if (zipCode.Length == 7)
                zipCode = $"0{zipCode}";

            var url = $"https://ws.apicep.com/cep/{zipCode}.json";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            string json = string.Empty;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                json = reader.ReadToEnd();
            }

            var addressJson = JsonConvert.DeserializeObject<AddressApi>(json);

            return new
                AddressApi(
                    status: addressJson.Status,
                    code: addressJson.Code,                  
                    city: addressJson.City,
                    state: addressJson.State,
                    district: addressJson.District,
                    address: addressJson.Address
                    );
        }
    }
}