using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TflApp.Library.Model.Configuration
{
    public interface IAppConfig
    {
        public string AppKey { get; set; }
        public string TflBaseAddress { get; set; }
        public string TflApi { get; set; }
    }
}
