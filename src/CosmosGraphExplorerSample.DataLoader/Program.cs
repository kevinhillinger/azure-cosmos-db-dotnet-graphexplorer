using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CosmosGraphExplorerSample.DataLoader
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new HostBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                    config.AddEnvironmentVariables("EXPLORE_COSMOSDB_");
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddOptions();
                    services.Configure<CosmosConfig>(c =>
                    {
                        // loads the configuration from the environment variables

                        c.DatabaseName = Environment.GetEnvironmentVariable("DATABASE_NAME");
                        c.GraphName = Environment.GetEnvironmentVariable("GRAPH_NAME");
                        c.Endpoint = Environment.GetEnvironmentVariable("ENDPOINT");
                        c.AuthKey = Environment.GetEnvironmentVariable("AUTH_KEY");
                    });

                    services.AddTransient<CosmosDataLoader>();
                    services.AddSingleton<IHostedService, HostService>();
                })
                .ConfigureLogging((hostingContext, logging) => {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                });

            await builder.RunConsoleAsync();
        }
    }
}
