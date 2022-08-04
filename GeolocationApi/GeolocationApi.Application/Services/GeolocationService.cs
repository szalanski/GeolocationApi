using GeolocationApi.Application.Contracts;
using GeolocationApi.Application.Models.GeolocationData;
using GeolocationApi.Application.Responses;
using Newtonsoft.Json;

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

        public async Task<GeolocationServiceResponse> GetAsync(string address, CancellationToken cancelaltionToken)
        {
            cancelaltionToken.ThrowIfCancellationRequested();

            var url = $"http://api.ipstack.com/{address}?access_key={_apiKey}";
            using var response = await _client.GetAsync(url, cancelaltionToken);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync(cancelaltionToken);
                var error = JsonConvert.DeserializeObject<ErrorResponse>(json);
                if (error.Success.HasValue)
                    return new GeolocationServiceResponse(null, response.StatusCode, false, error.Error.Info);

                var content = JsonConvert.DeserializeObject<GeolocationModel>(json);
                return new GeolocationServiceResponse(content, response.StatusCode);
            }

            return new GeolocationServiceResponse(null, response.StatusCode, false);
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
