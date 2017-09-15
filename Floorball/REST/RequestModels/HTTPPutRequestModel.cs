using System;
namespace Floorball.REST.RequestModels
{
    public class HTTPPutRequestModel<T> : RequestModel
    {
        
		public T Body { get; set; }

    }
}
