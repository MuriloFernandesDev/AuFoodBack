using AuFood.Models;
using Azure.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AuFood.Auxiliary
{
    public class ViaCepResponse
    {
        public string cep { get; set; }
        public string logradouro { get; set; }
        public string bairro { get; set; }
        public string localidade { get; set; }
        public string uf { get; set; }
    }
    
    public class Connect
    {
        public async Task<ViaCepResponse> GetAddress(int zip_code)
        {
            var client = new HttpClient();
            
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://viacep.com.br/ws/" + zip_code + "/json/"),
                Headers =
                {
                    { "x-rapidapi-key", "SIGN-UP-FOR-KEY" },
                    { "x-rapidapi-host", "example.com" },
                },
            };
            
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var retorno = JsonConvert.DeserializeObject<ViaCepResponse>(body);

                if (retorno.cep == null)
                    throw new Exception("CEP não encontrado");

                return retorno;
            }
        }
    }
}
