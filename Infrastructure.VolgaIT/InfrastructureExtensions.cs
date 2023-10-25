using Infrastructure.VolgaIT.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.VolgaIT
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            return services.AddScoped<UserRepository>()
                .AddScoped<TransportRepository>()
                .AddScoped<RentRepository>();
        }
    }
}
