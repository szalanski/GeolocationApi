using GeolocationApi.Application.Contracts.Persistence;
using GeolocationApi.Persistence.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GeolocationApi.Persistence.EF
{
    public static class GeolocationEFPersistenceInstaller
    {
        public static IServiceCollection AddCeolocationEF(this IServiceCollection services)
        {
            services.AddDbContext<GeolocationContext>(options => options.UseSqlite());
            services.AddScoped<IGeolocationRepository, GeolocationRepository>();
            return services;
        }
    }
}
