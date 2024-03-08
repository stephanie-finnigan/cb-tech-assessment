using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TflApp.Library.Model;
using TflApp.Library.Model.Configuration;
using TflApp.Library.Model.Json;

namespace TflApp.Library.BusinessLogic
{
    public interface ITflRoadLogic
    {
        Task<StatusResponse> GetRoadStatuses(StatusRequest request);
    }

    public class TflRoadLogic : ITflRoadLogic
    { 
        private readonly IConfiguration _config;
        private readonly ILogger<TflRoadLogic> _logger;

        public TflRoadLogic(IConfiguration config, ILogger<TflRoadLogic> logger)
        {
            _config = config;
            _logger = logger;
        }

        public async Task<StatusResponse> GetRoadStatuses(StatusRequest request)
        {
            using var _client = new HttpClient();
            var _app = _config.GetRequiredSection("AppConfig").Get<AppConfig>();

            var ids = string.Join(",", request.RoadIds);
            var statuses = new List<AggregatedRoadStatus>();
            var error = new TflErrorResponse();

            try
            {
                _logger.LogInformation("Tfl request initiated.");

                var response = await _client.GetAsync($"{_app.TflBaseAddress}/{_app.TflApi}/{ids}/Status?" +
                    $"startDate={request.StartDate}&endDate={request.EndDate}&app_key={_app.AppKey}");

                if (response.IsSuccessStatusCode)
                {
                    var stringResp = await response.Content.ReadAsStringAsync();
                    var tflData = JsonConvert.DeserializeObject<List<JsonTflDeserializer>>(stringResp);

                    foreach (var data in tflData)
                    {
                        var status = new AggregatedRoadStatus
                        {
                            DisplayName = data.DisplayName,
                            RoadStatus = data.StatusSeverity,
                            RoadStatusDescription = data.StatusSeverityDescription
                        };
                        statuses.Add(status);
                    }
                }
                else
                {
                    var stringResp = await response.Content.ReadAsStringAsync();
                    var tflError = JsonConvert.DeserializeObject<JsonTflErrorDeserializer>(stringResp);

                    error = new TflErrorResponse
                    {
                        ExceptionType = tflError.ExceptionType,
                        StatusCode = tflError.StatusCode,
                        HttpStatus = tflError.HttpStatus,
                        Message = tflError.Message
                    };
                }
            }
            catch (Exception ex)
            {
                return new StatusResponse { };
            }

            return new StatusResponse
            {
                RoadStatuses = statuses,
                ErrorResponse = error
            };
        }
    }
}
