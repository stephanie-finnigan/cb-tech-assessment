using BoDi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TflApp.Library.BusinessLogic;
using TflApp.Library.Model.Configuration;

namespace TflApp.Test.Utilities
{
    public class AppFactory
    {
        private readonly IObjectContainer _objContainer;
        private const string appConfigFile = "appsettings.json";

        public AppFactory(IObjectContainer objContainer)
        {
            _objContainer = objContainer;
        }

        [BeforeScenario]
        public void RegisterServices()
        {
            var factory = GetWebApplicationFactory();
            _objContainer.RegisterInstanceAs(factory);
            var tflRoadLogic = (ITflRoadLogic)factory.Services.GetService(typeof(ITflRoadLogic))!;
            _objContainer.RegisterInstanceAs(tflRoadLogic);
        }

        private WebApplicationFactory<Program> GetWebApplicationFactory() =>
            new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    IConfigurationSection? configSection = null;
                    builder.ConfigureAppConfiguration((_, config) =>
                    {
                        config.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), appConfigFile));
                        configSection = _.Configuration.GetSection(nameof(AppConfig));
                    });
                    builder.ConfigureTestServices(services =>
                    {
                        services.Configure<AppConfig>(configSection);
                    });
                });
    }
}
