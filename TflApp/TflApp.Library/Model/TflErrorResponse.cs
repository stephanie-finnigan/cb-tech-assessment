using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TflApp.Library.Model
{
    public class TflErrorResponse
    {
        public int ErrorCode { get; set; }
        public string ExceptionType { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string HttpStatus { get; set; }
        public string Message { get; set; }
    }
}
