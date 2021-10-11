using Conteo_y_recaudo.Data;
using Conteo_y_recaudo.Interfaces;
using Conteo_y_recaudo.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Conteo_y_recaudo.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IRecaudoService, RecaudoService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            return services;
        }
    }
}
