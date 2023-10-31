using App.VolgaIT.Services;
using App.VolgaIT.Services.PaymentServices;
using App.VolgaIT.Services.RentServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.VolgaIT
{
    public static class AppExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services.AddScoped<RentService>()
                .AddScoped<PaymentService>()
                .AddScoped<AdminTransportService>()
                .AddScoped<TransportService>()
                .AddScoped<AdminUserService>()
                .AddScoped<UserService>()
                .AddScoped<AdminRentService>();

        }
    }
}
