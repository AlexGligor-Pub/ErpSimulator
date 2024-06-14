using Microsoft.Extensions.DependencyInjection;

using Microsoft.EntityFrameworkCore;
using Domain.Interfaces;
using Infrastructure.Services;
using Infrastructure.Configuration;
using System.Configuration;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            var configService = new ConfigurationManagerService();
            services.AddDbContext<SqlDbContext>(options => options.UseSqlServer(configService.GetConnectionString("DefaultConnection")));
            services.AddScoped<IDemoOrderService, DemoOrderService>();
            services.AddScoped<UnsOrderService>();
            services.AddScoped<OrdersBucketService>();
            services.AddScoped<OrderStateMachineService>();
            services.AddScoped<BucketOrderProcessorService>();
            services.AddScoped<ConfigurationManagerService>();
            services.AddScoped<MQTTPublisher>();
            return services;
        }
    }
}
