using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TflApp.Library.BusinessLogic;
using TflApp.Library.Model.Configuration;

using IAppConfig = TflApp.Library.Model.Configuration.IAppConfig;

namespace TflApp
{
    public partial class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();

            var host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((_, builder) =>
                {
                    builder.SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    var config = builder.Build();
                })
                .ConfigureServices((_, services) =>
                {
                    services.AddTransient<IClientApp, ClientApp>();
                    services.AddTransient<ITflRoadLogic, TflRoadLogic>();
                    services.AddOptions();
                    services.Configure<AppConfig>(_.Configuration.GetSection("AppConfig"));
                })
                .Build();

            var app = ActivatorUtilities.CreateInstance<ClientApp>(host.Services);
            app.Run();
        }
    }
}
