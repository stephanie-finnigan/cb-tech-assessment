using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TflApp.Library.Model
{
    public class StatusResponse
    {
        public List<AggregatedRoadStatus> RoadStatuses { get; set; }
        public TflErrorResponse ErrorResponse { get; set; }
    }
}
