using Microsoft.Extensions.Configuration;
using Serilog;
using Topper;

namespace ServiceSalesProcessor
{
    static class Program
    {
        private static IConfiguration Configuration { get; set; }

        static void Main()
        {
            Log.Logger = new LoggerConfiguration()
               .WriteTo.Console()
               .CreateLogger();

            ReadConfiguration();

            var serviceConfiguration = new ServiceConfiguration()
               .Add("OurBackendBus", () => new Backend(Configuration));

            ServiceHost.Run(serviceConfiguration);
        }

        private static void ReadConfiguration()
        {
            var profile       = System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var isDevelopment = profile == "Development";

            var builder =
                new ConfigurationBuilder()
                   .AddJsonFile("appsettings.json", optional: false)
                   .AddEnvironmentVariables();

            if (isDevelopment)
            {
                builder
                   .AddUserSecrets(System.Reflection.Assembly.GetEntryAssembly(), optional: true);
            }

            Configuration = builder.Build();
        }
    }
}
