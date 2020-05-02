using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BookLibrary.Management.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var builtConfig = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddCommandLine(args)
                .AddEnvironmentVariables()
                .Build();

            return Host.CreateDefaultBuilder(args)
                    .ConfigureLogging(logging =>
                    {
                        logging.AddConfiguration(builtConfig.GetSection("Logging"));
                        logging.ClearProviders();
                        logging.AddConsole();
                    })
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseStartup<Startup>();
                    });
        }
    }
}
