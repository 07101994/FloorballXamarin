using System;
using System.Net;
using System.Threading.Tasks;
using Floorball.Exceptions;
using Floorball.REST.RequestModels;
using Newtonsoft.Json;
using RestSharp;

namespace Floorball.REST.RESTManagers
{
    public abstract class RESTManagerBase : IRESTManager
    {
		protected static FloorballSerializer deserial = new FloorballSerializer(new JsonSerializer());
		protected static string ServerURL = "https://floorball.azurewebsites.net";
		//private static string ServerURL = "http://192.168.0.20:8080";
		//private static string ServerURL = "http://192.168.173.1:8088";
		//private static string ServerURL = "http://192.168.173.1:8088";

		protected static void CheckError(RestResponse response, string message)
		{
			if (response.StatusCode == HttpStatusCode.InternalServerError)
			{
				throw new FloorballException(message, null);
			}
			else
			{
				if (response.ErrorException != null)
				{
					throw new CommunicationException(response.ErrorMessage, response.ErrorException);
				}
			}
		}

        public abstract HTTPDeleteRequestModel Delete(HTTPDeleteRequestModel request);
        public abstract Task<HTTPDeleteRequestModel> DeleteAsync(HTTPDeleteRequestModel request);

        public abstract T Get<T>(HTTPGetRequestModel request);
        public abstract Task<T> GetAsync<T>(HTTPGetRequestModel request);

        public abstract T2 Post<T1, T2>(HTTPPostRequestModel<T1> request);
        public abstract Task<T2> PostAsync<T1, T2>(HTTPPostRequestModel<T1> request);

        public abstract T2 Put<T1, T2>(HTTPPutRequestModel<T1> request);
        public abstract Task<T2> PutAsync<T1, T2>(HTTPPutRequestModel<T1> request);
    }
}
