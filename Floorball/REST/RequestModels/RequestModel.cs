using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Floorball.REST
{
    public abstract class RequestModel
    {
        public string Url { get; set; }

        public string ErrorMsg { get; set; }
		
        public Dictionary<string, string> UrlParams { get; set; }

        public Dictionary<string, string> Headers { get; set; }

	}
}
