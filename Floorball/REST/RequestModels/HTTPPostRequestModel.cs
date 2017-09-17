using System;
namespace Floorball.REST.RequestModels
{
    public class HTTPPostRequestModel<T> : RequestModel
    {
        public T Body { get; set; }
    }
}
