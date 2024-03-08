using Microsoft.Extensions.Logging;
using TflApp.Library.BusinessLogic;
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

            var request = new StatusRequest
            {
                RoadIds = new List<string> { "A2" },
                StartDate = "2024-02-05",
                EndDate = "2024-02-06"
            };

            var result = _logic.GetRoadStatuses(request).Result;

            Console.WriteLine($"Successfully retrieved road statuses: {result.RoadStatuses.FirstOrDefault().DisplayName}");
        }
    }
}
