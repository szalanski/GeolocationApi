using GeolocationApi.Application.Responses;

namespace GeolocationApi.Application.Contracts
{
    public interface IGeolocationService : IDisposable
    {
        Task<GeolocationServiceResponse> GetAsync(string url);
    }
}
