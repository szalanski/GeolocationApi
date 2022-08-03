using GeolocationApi.Application.Contracts;
using GeolocationApi.Application.Models.GeolocationData;
using GeolocationApi.Application.Responses;

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

        public async Task<GeolocationServiceResponse> GetAsync(string address)
        {
            var url = $"http://api.ipstack.com/{address}?access_key={_apiKey}";
            using var response = await _client.GetAsync(url);

            if(response.IsSuccessStatusCode)
            {
                var content =  await response.Content.ReadAsAsync<GeolocationModel>();
                return new GeolocationServiceResponse(content, response.StatusCode);
            }

            return new GeolocationServiceResponse(null, response.StatusCode);
        }


        public void Dispose()
        {
            if (_client != null)
            {
                _client.Dispose();
            }
        }
    }
}
