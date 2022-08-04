using ColocationApi.Domain.Entities;

namespace GeolocationApi.Application.Contracts.Persistence
{
    public interface IGeolocationRepository
    {
        Task<Geolocation> GetByIpAsync(string ip);

        Task<IReadOnlyList<Geolocation>> GetAllAsync();

        Task<Geolocation> AddAsync(Geolocation entity);

        Task<Geolocation> UpdateAsync(Geolocation entity);

        Task<Geolocation> DeleteAsync(Geolocation entity);
    }
}
