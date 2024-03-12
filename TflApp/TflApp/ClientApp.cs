using Microsoft.Extensions.Logging;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TflApp.Library.BusinessLogic;
using TflApp.Library.Helpers;
using TflApp.Library.Model;

namespace TflApp
{
    public class ClientApp : IClientApp
    {
        private readonly ILogger<ClientApp> _logger;
        private readonly ITflRoadLogic _logic;

        public ClientApp(ILogger<ClientApp> logger, ITflRoadLogic logic)
        {
            _logger = logger;
            _logic = logic;
        }

        public void Run()
        {
            _logger.LogInformation("Running client application");

            var testReq = new StatusRequest
            {
                RoadIds = new List<string> { "A2" },
                StartDate = "2024-02-05",
                EndDate = "2024-02-06"
            };

            var testErrReq = new StatusRequest
            {
                RoadIds = new List<string> { "XY" }
            };

            try
            {
                var result = _logic.GetRoadStatuses(testErrReq).Result;

                if (result.RoadStatuses.Count != 0)
                {
                    var ars = result.RoadStatuses.FirstOrDefault();

                    Console.WriteLine("Successfully retrieved road statuses!");
                    Console.WriteLine($"{Helper.DisplayName<AggregatedRoadStatus>(nameof(AggregatedRoadStatus.DisplayName))}: {ars.DisplayName}");
                    Console.WriteLine($"{Helper.DisplayName<AggregatedRoadStatus>(nameof(AggregatedRoadStatus.RoadStatus))}: {ars.RoadStatus}");
                    Console.WriteLine($"{Helper.DisplayName<AggregatedRoadStatus>(nameof(AggregatedRoadStatus.RoadStatusDescription))}: {ars.RoadStatusDescription}");
                }
                else
                {
                    Console.WriteLine("Failed to retrieve road statuses!");
                    Console.WriteLine($"Error code: {result.ErrorResponse.StatusCode} with message: {result.ErrorResponse.Message}");
                    //Environment.Exit();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
