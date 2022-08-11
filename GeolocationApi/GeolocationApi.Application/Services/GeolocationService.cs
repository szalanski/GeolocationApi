using GeolocationApi.Application.Contracts;
using GeolocationApi.Application.Exceptions;
using GeolocationApi.Application.Models.GeolocationData;
using LanguageExt.Common;
using Newtonsoft.Json;

namespace GeolocationApi.Application.Services
{
    public class GeolocationService : IGeolocationService
    {
        private const int InvalidIpErrorCode = 106;
        private readonly HttpClient _client;
        private readonly string _apiKey;

        public GeolocationService(HttpClient httpClient, string apiKey)
        {
            _client = httpClient;
            _apiKey = apiKey;
        }

        public async Task<Result<GeolocationModel>> GetAsync(string address, CancellationToken cancelaltionToken)
        {
            cancelaltionToken.ThrowIfCancellationRequested();

            var url = $"http://api.ipstack.com/{address}?access_key={_apiKey}";
            using var response = await _client.GetAsync(url, cancelaltionToken);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync(cancelaltionToken);
                var error = JsonConvert.DeserializeObject<ErrorResponse>(json);
                if (error.Success.HasValue)
                {
                    return new Result<GeolocationModel>(HandleErrorResponse(error));
                }

                var content = JsonConvert.DeserializeObject<GeolocationModel>(json);
                return new Result<GeolocationModel>(content);
            }

            return new Result<GeolocationModel>(new InternalErrorException(response.ReasonPhrase));
        }

        private Exception HandleErrorResponse(ErrorResponse response)
        {
            if (response.Error.Code == InvalidIpErrorCode)
                return new BadRequestException(response.Error.Info);

            return new InternalErrorException(response.Error.Info);
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
