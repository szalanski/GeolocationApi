using ColocationApi.Domain.Entities;

namespace GeolocationApi.Application.Contracts.Persistence
{
    public interface IGeolocationRepository
    {
        Task<Geolocation> GetByIpAsync(string ip, CancellationToken cancellationToken);

        Task<Geolocation> GetByUrlAsync(string url, CancellationToken cancellationToken);

        Task<IReadOnlyList<Geolocation>> GetAllAsync(CancellationToken cancellationToken);

        Task<Geolocation> AddAsync(Geolocation entity, CancellationToken cancellationToken);
        Task<Geolocation> DeleteAsync(Geolocation entity, CancellationToken cancellationToken);
    }
}
