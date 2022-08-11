using System.Reflection;
using GeolocationApi.Application.Contracts;
using GeolocationApi.Application.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace GeolocationApi.Application
{
    public static class ApplicationInstaller
    {
        public static IServiceCollection AddGeolocationApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddHttpClient<IGeolocationService, GeolocationService>();
            services.AddSingleton<IGeolocationService, GeolocationService>(services =>
            {
                var client = services.GetRequiredService<HttpClient>();
                var apiKey = configuration["ipstackApiKey"];
                return new GeolocationService(client, apiKey);
            });
            return services;
        }
    }
}
