using System;
using System.Collections.Generic;

namespace Floorball.REST.RequestModels
{
    public class HTTPGetRequestModel<T1> : RequestModel
    {
        public T1 Response { get; set; }

        public Dictionary<string, string> QueryParams { get; set; }
    }
}
