using System;
using System.Net.Http;
using System.Threading.Tasks;
using Floorball.REST.RequestModels;

namespace Floorball.REST.RESTManagers
{
    public partial class RESTManager : RESTManagerBase
    {

        public override async Task<T> GetAsync<T>(HTTPGetRequestModel request)
        {
            var response = await client.GetAsync(CreateGetUri(request));
            return await response.EnsureSuccessStatusCode().Content.ReadAsAsync<T>();
        }

        public override async Task<T2> PostAsync<T1, T2>(HTTPPostRequestModel<T1> request) 
        {
            var response = await client.PostAsJsonAsync(CreatePostUri(request), request.Body);
            return await response.EnsureSuccessStatusCode().Content.ReadAsAsync<T2>();
        }

		public override async Task<T2> PutAsync<T1, T2>(HTTPPutRequestModel<T1> request)
		{
			var response = await client.PutAsJsonAsync(CreatePutUri(request), request.Body);
			return await response.EnsureSuccessStatusCode().Content.ReadAsAsync<T2>();
		}

		public override async Task<HTTPDeleteRequestModel> DeleteAsync(HTTPDeleteRequestModel request)
		{
			var response = await client.DeleteAsync(CreateDeleteUri(request));
			return await response.EnsureSuccessStatusCode().Content.ReadAsAsync<HTTPDeleteRequestModel>();
		}
    }
}
