﻿using Newtonsoft.Json;
using SalesProject.Domain.Dtos;
using SalesProject.Domain.Extensions;
using SalesProject.Domain.Interfaces.Service;
using System.IO;
using System.Net;

namespace SalesProject.Application.Services
{
    public class CnpjApiService : ICnpjApiService
    {
        public CnpjApi CompleteCustomerApi(string cnpj)
        {
            cnpj = cnpj.CleanCnpjToUseOnUrl();

            var url = $"https://www.receitaws.com.br/v1/cnpj/{cnpj}";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            string json = string.Empty;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                json = reader.ReadToEnd();
            }

            var customerResponse = JsonConvert.DeserializeObject<CnpjApi>(json);

            return new
                CnpjApi(
                    status: customerResponse.Status,
                    cnpj: customerResponse.Cnpj,
                    nome: customerResponse.Nome,
                    telefone: customerResponse.Telefone,
                    email: customerResponse.Email,
                    abertura: customerResponse.Abertura,
                    message: customerResponse.Message ?? string.Empty
                    );
        }
    }
}