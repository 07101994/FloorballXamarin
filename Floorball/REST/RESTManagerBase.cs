using System;
using System.Net;
using Floorball.Exceptions;
using Newtonsoft.Json;
using RestSharp;

namespace Floorball.REST
{
    public abstract class RESTManagerBase
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
    }
}
