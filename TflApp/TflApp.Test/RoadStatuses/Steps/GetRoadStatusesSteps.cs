using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using TechTalk.SpecFlow;
using TflApp.Library.BusinessLogic;
using TflApp.Library.Model;
using TflApp.Library.Model.Configuration;

namespace TflApp.Test.RoadStatuses.Steps
{
    [Binding, Scope(Feature = "Get Road Statuses")]
    internal class GetRoadStatusesSteps
    {
        private IConfiguration _config;
        private TflRoadLogic _logic;
        private ILogger<TflRoadLogic> _logger = new NullLogger<TflRoadLogic>();

        private StatusRequest _getRequest;
        private StatusResponse _getResponse;

        public GetRoadStatusesSteps(IConfiguration config)
        {
            _config = config;
            _logic = new TflRoadLogic(_config, _logger);
        }

        [Given(@"A valid road id ""(.*)""")]
        public void GivenAValidRoadId(string roadId)
        {
            _getRequest = new StatusRequest
            {
                RoadIds = new List<string> { roadId }
            };
        }

        [Given(@"A valid road id and date range")]
        public void GivenAValidRoadIdAndDateRange(Table table)
        {
            var row = table.Rows.First();
            _getRequest = new StatusRequest
            {
                RoadIds = new List<string> { row["Road id"] },
                StartDate = row["Start date"],
                EndDate = row["End date"]
            };
        }

        [Given(@"An invalid road id ""(.*)""")]
        public void GivenAnInvalidRoadId(string roadId)
        {
            _getRequest = new StatusRequest
            {
                RoadIds = new List<string> { roadId }
            };
        }

        [When(@"The client runs")]
        public async Task WhenTheClientRuns()
        {
            _getResponse = await _logic.GetRoadStatuses(_getRequest);
        }

        [Then(@"The road display name is ""(.*)""")]
        public void ThenTheRoadDisplayNameIs(string expDisplayName)
        {
            foreach (var data in _getResponse.RoadStatuses)
            {
                data.DisplayName.Should().Be(expDisplayName);
            }

            _getResponse.ErrorResponse.ExceptionType.Should().BeNullOrEmpty();
        }

        [Then(@"The road status severity is displayed as ""(.*)""")]
        public void ThenTheRoadStatusSeverityIsDisplayedAs(string expSeverity)
        {
            foreach (var data in _getResponse.RoadStatuses)
            {
                data.RoadStatus.Should().Be(expSeverity);
            }

            _getResponse.ErrorResponse.ExceptionType.Should().BeNullOrEmpty();
        }

        [Then(@"The road status severity description is displayed as ""(.*)""")]
        public void ThenTheRoadStatusSeverityDescriptionIsDisplayedAs(string expSeverityDesc)
        {
            foreach (var data in _getResponse.RoadStatuses)
            {
                data.RoadStatusDescription.Should().Be(expSeverityDesc);
            }

            _getResponse.ErrorResponse.ExceptionType.Should().BeNullOrEmpty();
        }

        [Then(@"The response is returned with informative error message ""(.*)""")]
        public void ThenTheResponseIsReturnedWithInformativeErrorMessage(string expMessage)
        {
            _getResponse.ErrorResponse.Message.Should().Be(expMessage);
        }

        [Then(@"The application exits with system error code")]
        public void ThenTheApplicationExitsWithSystemErrorCode()
        {
            throw new PendingStepException();
        }

        [Then(@"The response is returned with error message ""(.*)""")]
        public void ThenTheResponseIsReturnedWithErrorMessage(string expMessage)
        {
            _getResponse.ErrorResponse.Message.Should().Be(expMessage);
        }

    }
}