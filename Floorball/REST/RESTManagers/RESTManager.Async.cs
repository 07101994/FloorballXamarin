using System;
using System.Threading.Tasks;
using Floorball.REST.RequestModels;
using RestSharp;

namespace Floorball.REST.RESTManagers
{
    public partial class RESTManager : RESTManagerBase
    {

        public override async Task<T> GetAsync<T>(HTTPGetRequestModel request)
        {

            try
            {
                FloorballRESTClient client = new FloorballRESTClient(ServerURL);

                RestResponse response = await client.ExecuteRequestAsync(request.Url, Method.GET, request.UrlParams, request.QueryParams, null, request.Headers) as RestResponse;

                CheckError(response, request.ErrorMsg);

                return deserial.Deserialize<T>(response);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public override async Task<T2> PostAsync<T1, T2>(HTTPPostRequestModel<T1> request) {


			try
			{
				FloorballRESTClient client = new FloorballRESTClient(ServerURL);

				RestResponse response = await client.ExecuteRequestAsync(request.Url, Method.POST, request.UrlParams, null, request.Body, request.Headers) as RestResponse;

				CheckError(response, request.ErrorMsg);

                return deserial.Deserialize<T2>(response);
			}
			catch (Exception ex)
			{
				throw ex;
			}


        }

		public override async Task<T2> PutAsync<T1, T2>(HTTPPutRequestModel<T1> request)
		{

			try
			{
				FloorballRESTClient client = new FloorballRESTClient(ServerURL);

				RestResponse response = await client.ExecuteRequestAsync(request.Url, Method.PUT, request.UrlParams, null, request.Body, request.Headers) as RestResponse;

				CheckError(response, request.ErrorMsg);

				return deserial.Deserialize<T2>(response);

			}
			catch (Exception ex)
			{
				throw ex;
			}

		}

		public override async Task<HTTPDeleteRequestModel> DeleteAsync(HTTPDeleteRequestModel request)
		{

			try
			{
				FloorballRESTClient client = new FloorballRESTClient(ServerURL);

				RestResponse response = await client.ExecuteRequestAsync(request.Url, Method.DELETE, request.UrlParams, null, null, request.Headers) as RestResponse;

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
