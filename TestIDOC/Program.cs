using System;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Infrastructure.SAPDM;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace TestIDOC
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            var host = CreateHostBuilder().Build();

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var mainForm = host.Services.GetRequiredService<Form1>();
            Application.Run(mainForm);
        }

        static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddHttpClient(); // Adaugă HttpClient
                    services.AddTransient<Form1>();
                    var baseAddress = @"https://localhost:7278/";
                    var tokenUrl = @"https://accenture-prod-opsmessolution-va627rp8.authentication.eu10.hana.ondemand.com/oauth/token";
                    var clientId = "sb-13d86547-8f78-40a6-814c-0e95af40baf6!b153259|it-rt-accenture-prod-opsmessolution-va627rp8!b117912";
                    var clientSecret = "cba6ed0f-7f06-4f8a-88ec-fd35f3a4548e$-iKZJ-WMxh0eOqVMTXj6IG2ysdEJEXzZMTaReghSpZI=";


                    services.AddHttpClient("YourAPI", client =>
                    {
                        client.BaseAddress = new Uri(baseAddress);
                    }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
                    {
                        ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                    });
                    services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("YourAPI"));

                    services.AddTransient(sp => new AuthService(
                        sp.GetRequiredService<IHttpClientFactory>().CreateClient("YourAPI"),
                        clientId,
                        clientSecret,
                        tokenUrl
                    ));

                    services.AddScoped<ApiService>();
                });
        }
    }
}
