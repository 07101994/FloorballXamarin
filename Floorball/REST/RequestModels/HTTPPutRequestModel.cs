using System;
namespace Floorball.REST.RequestModels
{
    public class HTTPPutRequestModel<T1, T2> : RequestModel
    {
        
		public T1 Body { get; set; }

		public T2 Response { get; set; }

    }
}
