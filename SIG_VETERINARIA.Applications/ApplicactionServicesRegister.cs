using Microsoft.Extensions.DependencyInjection;
using SIG_VETERINARIA.Abstrations.Applications;
using SIG_VETERINARIA.Applications.User;

namespace SIG_VETERINARIA.Applications
{
    public static class ApplicactionServicesRegister
    {
        public static IServiceCollection AddApplicationRegister(this IServiceCollection services)
        {
            services.AddScoped<IUserApplications,UserApplications>();
            return services;
        }
    }
}
