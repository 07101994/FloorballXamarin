using System;
using System.Collections.Generic;

namespace Floorball.REST.RequestModels
{
    public class HTTPGetRequestModel : RequestModel
    {

        public Dictionary<string, string> QueryParams { get; set; }
    }
}
