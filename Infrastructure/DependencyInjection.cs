using Microsoft.Extensions.DependencyInjection;

using Microsoft.EntityFrameworkCore;
using Domain.Interfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Configuration;
using Infrastructure.SAPDM;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddDbContext<SqlDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IDemoOrderService, DemoOrderService>();
            services.AddScoped<UnsOrderService>();
            services.AddScoped<OrdersBucketService>();
            services.AddScoped<OrderStateMachineService>();
            services.AddScoped<BucketOrderProcessorService>();
            services.AddScoped<MQTTPublisher>();

            var baseAddress = builder.Configuration.GetValue<string>("ApiSettings:BaseAddress");
            var tokenUrl = builder.Configuration.GetValue<string>("ApiSettings:TokenUrl");
            var clientId = builder.Configuration.GetValue<string>("ApiSettings:ClientId");
            var clientSecret = builder.Configuration.GetValue<string>("ApiSettings:ClientSecret");

            builder.Services.AddHttpClient("YourAPI", client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            });
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("YourAPI"));

            builder.Services.AddTransient(sp => new AuthService(
                sp.GetRequiredService<IHttpClientFactory>().CreateClient("YourAPI"),
                clientId,
                clientSecret,
                tokenUrl
            ));

            services.AddScoped<ApiService>();

            return services;
        }
    }
}
