using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;


namespace GeolocationApi.Application
{
    public static class ApplicationInstaller
    {
        public static IServiceCollection AddGeolocationApi(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
