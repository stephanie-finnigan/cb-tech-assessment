using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TflApp.Library.Model
{
    public class AggregatedRoadStatus
    {
        [DisplayName("Display Name")]
        public string DisplayName { get; set; }

        [DisplayName("Road Status")]
        public string RoadStatus { get; set; }

        [DisplayName("Road Status Description")]
        public string RoadStatusDescription { get; set; }
    }
}
