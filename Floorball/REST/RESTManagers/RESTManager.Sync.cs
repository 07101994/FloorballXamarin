using System;
using Floorball.REST.RequestModels;
using RestSharp;

namespace Floorball.REST.RESTManagers
{
    public partial class RESTManager : RESTManagerBase
	{
		public override T Get<T>(HTTPGetRequestModel request)
		{

			try
			{
				FloorballRESTClient client = new FloorballRESTClient(ServerURL);

				RestResponse response = client.ExecuteRequest(request.Url, Method.GET, request.UrlParams, request.QueryParams, null, request.Headers) as RestResponse;

				CheckError(response, request.ErrorMsg);

				return deserial.Deserialize<T>(response);

			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public override T2 Post<T1, T2>(HTTPPostRequestModel<T1> request)
		{


			try
			{
				FloorballRESTClient client = new FloorballRESTClient(ServerURL);

				RestResponse response = client.ExecuteRequest(request.Url, Method.POST, request.UrlParams, null, request.Body, request.Headers) as RestResponse;

				CheckError(response, request.ErrorMsg);

				return deserial.Deserialize<T2>(response);

			}
			catch (Exception ex)
			{
				throw ex;
			}


		}

		public override T2 Put<T1, T2>(HTTPPutRequestModel<T1> request)
		{

			try
			{
				FloorballRESTClient client = new FloorballRESTClient(ServerURL);

				RestResponse response = client.ExecuteRequest(request.Url, Method.PUT, request.UrlParams, null, request.Body, request.Headers) as RestResponse;

				CheckError(response, request.ErrorMsg);

				return deserial.Deserialize<T2>(response);
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public override HTTPDeleteRequestModel Delete(HTTPDeleteRequestModel request)
		{

			try
			{
				FloorballRESTClient client = new FloorballRESTClient(ServerURL);

				RestResponse response = client.ExecuteRequest(request.Url, Method.DELETE, request.UrlParams, null, null, request.Headers) as RestResponse;

				CheckError(response, request.ErrorMsg);

				return request;
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}



	}
}
