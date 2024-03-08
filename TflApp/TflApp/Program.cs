using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TflApp.Library.BusinessLogic;
using TflApp.Library.Model.Configuration;

using IAppConfig = TflApp.Library.Model.Configuration.IAppConfig;

namespace TflApp
{
    class Program
    {
        private readonly IConfiguration _configuration;
        private readonly IAppConfig _appConfig;

        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            BuildConfig(builder);

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((_, services) =>
                {
                    services.AddTransient<IClientApp, ClientApp>();
                    services.AddTransient<ITflRoadLogic, TflRoadLogic>();
                    services.AddTransient((s) => )
                })
                .Build();

            var app = ActivatorUtilities.CreateInstance<IClientApp>(host.Services);
            app.Run();
        }

        static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                   .Build();
        }
    }
}



//var services = scope.ServiceProvider;

//try
//{
//    services.GetRequiredService<ClientApp>().Run();
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}

//static IHostBuilder CreateHostBuilder()
//{
//    return Host.CreateDefaultBuilder()
//        .ConfigureServices((_, services) =>
//        {
//            services.AddSingleton<ClientApp>();
//            services.AddSingleton<ITflRoadLogic, TflRoadLogic>();
//            services.AddSingleton<IAppConfig, AppConfig>();
//            services.Configure<AppConfig>(_.Configuration.GetSection("AppConfig"));
//        });
//}
