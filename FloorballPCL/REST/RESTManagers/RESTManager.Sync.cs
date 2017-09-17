using System;
using System.Net.Http;
using Floorball.REST.RequestModels;

namespace Floorball.REST.RESTManagers
{
    public partial class RESTManager : RESTManagerBase
	{
		public override T Get<T>(HTTPGetRequestModel request)
		{
			var response = client.GetAsync(CreateGetUri(request)).Result;
			return response.EnsureSuccessStatusCode().Content.ReadAsAsync<T>().Result;
		}

		public override T2 Post<T1, T2>(HTTPPostRequestModel<T1> request)
		{
            var response = client.PostAsJsonAsync(CreatePostUri(request), request.Body).Result;
			return response.EnsureSuccessStatusCode().Content.ReadAsAsync<T2>().Result;
		}

		public override T2 Put<T1, T2>(HTTPPutRequestModel<T1> request)
		{
			var response = client.PutAsJsonAsync(CreatePutUri(request), request.Body).Result;
			return response.EnsureSuccessStatusCode().Content.ReadAsAsync<T2>().Result;
		}

		public override HTTPDeleteRequestModel Delete(HTTPDeleteRequestModel request)
		{
			var response = client.DeleteAsync(CreateDeleteUri(request)).Result;
			return response.EnsureSuccessStatusCode().Content.ReadAsAsync<HTTPDeleteRequestModel>().Result;

		}

	}
}
