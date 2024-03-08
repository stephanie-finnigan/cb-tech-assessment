using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TflApp.Library.Model
{
    public class StatusRequest
    {
        public List<string> RoadIds { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
