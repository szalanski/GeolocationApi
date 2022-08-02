using GeolocationApi.Application.Contracts;
using GeolocationApi.Application.Dtos.GeolocationData;

namespace GeolocationApi.Application.Services
{
    public class GeolocationService : IGeolocationService
    {
        private readonly HttpClient _client;
        private readonly string _apiKey;

        public GeolocationService(HttpClient httpClient, string apiKey)
        {
            _client = httpClient;
            _apiKey = apiKey;
        }

        public async Task<GeolocationDto> GetAsync(string address)
        {
            var url = $"http://api.ipstack.com/{address}?access_key={_apiKey}";
            using var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<GeolocationDto>();

        }   
    }
}
