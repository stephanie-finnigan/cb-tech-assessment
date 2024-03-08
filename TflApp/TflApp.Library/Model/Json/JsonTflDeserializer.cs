using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TflApp.Library.Model.Json
{
    public partial class JsonTflDeserializer
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("statusSeverity")]
        public string StatusSeverity { get; set; }

        [JsonProperty("statusSeverityDescription")]
        public string StatusSeverityDescription { get; set; }

        [JsonProperty("bounds")]
        public string Bounds { get; set; }

        [JsonProperty("envelope")]
        public string Envelope { get; set; }

        [JsonProperty("statusAggregationStartDate")]
        public string StatusAggregationStartDate { get; set; }

        [JsonProperty("statusAggregationEndDate")]
        public string StatusAggregationEndDate { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
