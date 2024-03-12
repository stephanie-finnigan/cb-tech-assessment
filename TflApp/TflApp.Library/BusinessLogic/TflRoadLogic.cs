using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
        private readonly AppConfig _config;
        private readonly ILogger<TflRoadLogic> _logger;

        public TflRoadLogic(IOptions<AppConfig> ops, ILogger<TflRoadLogic> logger)
        {
            _config = ops.Value;
            _logger = logger;
        }

        public async Task<StatusResponse> GetRoadStatuses(StatusRequest request)
        {
            using var _client = new HttpClient();

            var ids = string.Join(",", request.RoadIds);
            var statuses = new List<AggregatedRoadStatus>();
            var error = new TflErrorResponse();

            try
            {
                _logger.LogInformation("Tfl request initiated.");

                var response = await _client.GetAsync($"{_config.TflBaseAddress}/{_config.TflApi}/{ids}/Status?" +
                    $"startDate={request.StartDate}&endDate={request.EndDate}&app_key={_config.AppKey}");

                var stringResp = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
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
                    var tflError = JsonConvert.DeserializeObject<JsonTflErrorDeserializer>(stringResp);

                    error = new TflErrorResponse
                    {
                        ErrorCode = 1,
                        ExceptionType = tflError.ExceptionType,
                        StatusCode = tflError.StatusCode,
                        HttpStatus = tflError.HttpStatus,
                        Message = tflError.Message
                    };
                }
            }
            catch (HttpRequestException ex)
            {
                return new StatusResponse
                {
                    ErrorResponse = new TflErrorResponse
                    {
                        Message = ex.Message
                    }
                };
            }

            return new StatusResponse
            {
                RoadStatuses = statuses,
                ErrorResponse = error
            };
        }
    }
}
