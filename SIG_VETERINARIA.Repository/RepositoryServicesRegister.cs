using Microsoft.Extensions.DependencyInjection;
using SIG_VETERINARIA.Abstrations.Repository;
using SIG_VETERINARIA.Repository.User;

namespace SIG_VETERINARIA.Repository
{
    public static class RepositoryServicesRegister
    {
        public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserReposistory>();

            return services;
        }
    }
}
