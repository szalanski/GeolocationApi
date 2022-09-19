using GeolocationApi.Application.Contracts.Persistence;
using GeolocationApi.Persistence.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GeolocationApi.Persistence.EF
{
    public static class GeolocationEFPersistenceInstaller
    {
        public static IServiceCollection AddGeolocationEF(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<GeolocationContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IGeolocationRepository, GeolocationRepository>();
            return services;
        }
    }
}
