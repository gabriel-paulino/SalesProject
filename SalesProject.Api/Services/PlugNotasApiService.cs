using Microsoft.Extensions.Configuration;
using RestSharp;
using SalesProject.Domain.Entities;
using SalesProject.Domain.Response;
using SalesProject.Domain.Services;

namespace SalesProject.Api.Services
{
    public class PlugNotasApiService : IPlugNotasApiService
    {
        private readonly IConfiguration _configuration;

        public PlugNotasApiService(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public PlugNotasResponse SendInvoice(Invoice invoice)
        {
            //Todo....
            //Create an json object to send based in invoice object
            string jsonInvoice = string.Empty;
            //

            string url = "https://api.sandbox.plugnotas.com.br/nfe";
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("accept", "application/json");
            request.AddHeader("x-api-key", _configuration["PlugNotasApiKey"]);
            request.AddHeader("Content-Type", "application/json");
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(jsonInvoice);
            IRestResponse response = client.Execute(request);

            //Todo: Format Return
            return new PlugNotasResponse();

        }
    }
}
