using ColocationApi.Domain.Entities;
using GeolocationApi.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GeolocationApi.Persistence.EF.Repositories
{
    public class GeolocationRepository : IGeolocationRepository
    {
        private readonly GeolocationContext _dbContext;

        public GeolocationRepository(GeolocationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Geolocation> AddAsync(Geolocation entity, CancellationToken cancellationToken)
        {
            await _dbContext.Geolocations.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<Geolocation> DeleteAsync(Geolocation entity, CancellationToken cancellationToken)
        {
            _dbContext.Geolocations.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<IReadOnlyList<Geolocation>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Geolocations.ToListAsync();
        }

        public async Task<Geolocation> GetByIpAsync(string ip, CancellationToken cancellationToken)
        {
            return await _dbContext.Geolocations.FindAsync(ip);
        }

        public async Task<Geolocation> GetByUrlAsync(string url, CancellationToken cancellationToken)
        {
            return await _dbContext.Geolocations.FirstOrDefaultAsync(entity => entity.Url == url);
        }
    }
}
