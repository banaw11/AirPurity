using AirPurity.API.BusinessLogic.External.Interfaces;
using System.Net.Http.Json;

namespace AirPurity.API.BusinessLogic.External.Services
{
    public class GiosHttpClientContext : IHttpClientContext
    {
        private readonly HttpClient _httpClient;
        public GiosHttpClientContext(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("gios");
        }

        public async Task<T> Get<T>(string uri)
        {
            return await _httpClient.GetFromJsonAsync<T>(uri);
        }

        public async Task<T> Get<T>(string uri, object param)
        {
            return await _httpClient.GetFromJsonAsync<T>(uri + param);
        }
    }
}
