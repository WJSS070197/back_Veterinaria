using Microsoft.Extensions.DependencyInjection;
using SIG_VETERINARIA.Abstrations.Services;
using SIG_VETERINARIA.Services.User;

namespace SIG_VETERINARIA.Services
{
    public static class ServicesServicesRegister
    {
        public static IServiceCollection AddServicesServices(this IServiceCollection services)
        {
            services.AddScoped<IUserServices, UserServices>();
            return services;
        }
    }
}
