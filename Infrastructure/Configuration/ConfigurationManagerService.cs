using Microsoft.Extensions.Configuration;
using System.IO;

namespace Infrastructure.Configuration
{
    

    public class ConfigurationManagerService
    {
        public IConfiguration Configuration { get; }

        public ConfigurationManagerService()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }

        public string GetMySetting()
        {
            return Configuration["MySetting"];
        }

        public string GetConnectionString(string name)
        {
            return Configuration.GetConnectionString(name);
        }
    }

}
