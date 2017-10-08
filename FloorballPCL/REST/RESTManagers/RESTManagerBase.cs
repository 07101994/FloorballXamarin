using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Floorball.Exceptions;
using Floorball.REST.RequestModels;
using FloorballPCL.REST;
using Newtonsoft.Json;

namespace Floorball.REST.RESTManagers
{
    public abstract class RESTManagerBase : IRESTManager
    {
        //protected static FloorballSerializer deserial = new FloorballSerializer(new JsonSerializer());
        protected static string Port = "";
        //protected static string ServerUri = "floorball.azurewebsites.net";
        protected static string ServerUri = "floorballdemo.azurewebsites.net";
        protected static string Scheme = "https";

        protected static string BaseAddress
        {
            get
            {
                return Scheme + "://" + ServerUri + (Port != "" ? ":"+Port : "") + "/";                
            }
        }

        protected static HttpClient client;

        static RESTManagerBase()
        {
            client = new HttpClient();

            client.BaseAddress = new Uri(BaseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }

        public abstract HTTPDeleteRequestModel Delete(HTTPDeleteRequestModel request);
        public abstract Task<HTTPDeleteRequestModel> DeleteAsync(HTTPDeleteRequestModel request);

        public abstract T Get<T>(HTTPGetRequestModel request);
        public abstract Task<T> GetAsync<T>(HTTPGetRequestModel request);

        public abstract T2 Post<T1, T2>(HTTPPostRequestModel<T1> request);
        public abstract Task<T2> PostAsync<T1, T2>(HTTPPostRequestModel<T1> request);

        public abstract T2 Put<T1, T2>(HTTPPutRequestModel<T1> request);
        public abstract Task<T2> PutAsync<T1, T2>(HTTPPutRequestModel<T1> request);

        //protected static void CheckError(RestResponse response, string message)
        //{
        //    if (response.StatusCode == HttpStatusCode.InternalServerError)
        //    {
        //        throw new FloorballException(message, null);
        //    }
        //    else
        //    {
        //        if (response.ErrorException != null)
        //        {
        //            throw new CommunicationException(response.ErrorMessage, response.ErrorException);
        //        }
        //    }
        //}


        private UriBuilder CreateUriBuilder()
        {

			UriBuilder builder = new UriBuilder();

			//if (builder.Port != -1)
			//{
			//	builder.Port = Port;
			//}

			//builder.Scheme = Scheme;

            return builder;

        }


        private string AddPathParams(string uri, Dictionary<string,string> pathParams) 
        {
			if (pathParams != null)
			{
				foreach (var param in pathParams)
				{
					uri = uri.Replace("{" + param.Key + "}", param.Value);
				}
			}

            return uri;
        }

        private string AddQueryParams(string uri, Dictionary<string,string> queryParams)
        {
			if (queryParams != null)
			{
				StringBuilder query = new StringBuilder();

				foreach (var param in queryParams)
				{
					query = query.Append("&").Append(param.Key).Append("=").Append(param.Value);
				}

				if (queryParams.Count > 0)
				{
					uri += query.Remove(0, 1).Insert(0, "?").ToString();
				}
			}

            return uri;
        }

        protected string CreateGetUri(HTTPGetRequestModel request) 
        {
            //var str = CreateUriBuilder().AddPathParams(request.UrlParams).AddQueryParams(request.QueryParams).Uri;
            var str = AddQueryParams(AddPathParams(request.Url, request.UrlParams),request.QueryParams);
            return str;
        }

        protected Uri CreatePostUri<T>(HTTPPostRequestModel<T> request) 
        {
            return CreateUriBuilder().Uri;
        }

		protected Uri CreatePutUri<T>(HTTPPutRequestModel<T> request)
		{
			return CreateUriBuilder().Uri;
		}

		protected Uri CreateDeleteUri(HTTPDeleteRequestModel request)
		{
			return CreateUriBuilder().Uri;
		}

    }


}
