using GeolocationApi.Application.Dtos.GeolocationData;

namespace GeolocationApi.Application.Contracts
{
    public interface IGeolocationService
    {
        Task<GeolocationDto> GetAsync(string url);
    }
}
