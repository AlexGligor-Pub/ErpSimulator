using Microsoft.Extensions.DependencyInjection;

using Microsoft.EntityFrameworkCore;
using Domain.Interfaces;
using Infrastructure.Services;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string dbConnectionString)
        {
            services.AddDbContext<SqlDbContext>(options => options.UseSqlServer(dbConnectionString));
            services.AddScoped<IDemoOrderService, DemoOrderService>();
            services.AddScoped<UnsOrderService>();
            return services;
        }
    }
}
