using System;
using Floorball.REST.RequestModels;
using RestSharp;

namespace Floorball.REST
{
    public partial class RESTManager : RESTManagerBase
	{
		public HTTPGetRequestModel<T1> Get<T1>(HTTPGetRequestModel<T1> request)
		{

			try
			{
				FloorballRESTClient client = new FloorballRESTClient(ServerURL);

				RestResponse response = client.ExecuteRequest(request.Url, Method.GET, request.UrlParams, request.QueryParams, null, request.Headers) as RestResponse;

				CheckError(response, request.ErrorMsg);

				request.Response = deserial.Deserialize<T1>(response);

				return request;
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public HTTPPostRequestModel<T1, T2> Post<T1, T2>(HTTPPostRequestModel<T1, T2> request)
		{


			try
			{
				FloorballRESTClient client = new FloorballRESTClient(ServerURL);

				RestResponse response = client.ExecuteRequest(request.Url, Method.POST, request.UrlParams, null, request.Body, request.Headers) as RestResponse;

				CheckError(response, request.ErrorMsg);

				request.Response = deserial.Deserialize<T2>(response);

				return request;
			}
			catch (Exception ex)
			{
				throw ex;
			}


		}

		public HTTPPutRequestModel<T1, T2> Put<T1, T2>(HTTPPutRequestModel<T1, T2> request)
		{

			try
			{
				FloorballRESTClient client = new FloorballRESTClient(ServerURL);

				RestResponse response = client.ExecuteRequest(request.Url, Method.PUT, request.UrlParams, null, request.Body, request.Headers) as RestResponse;

				CheckError(response, request.ErrorMsg);

				request.Response = deserial.Deserialize<T2>(response);

				return request;
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public HTTPDeleteRequestModel Delete(HTTPDeleteRequestModel request)
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
