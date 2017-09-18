using System;
using System.Net.Http;
using Floorball.Exceptions;
using Floorball.REST.RequestModels;

namespace Floorball.REST.RESTManagers
{
    public partial class RESTManager : RESTManagerBase
	{
		public override T Get<T>(HTTPGetRequestModel request)
		{
            try
            {
				var response = client.GetAsync(CreateGetUri(request)).Result;
				return response.EnsureSuccessStatusCode().Content.ReadAsAsync<T>().Result;
            }
            catch (Exception ex)
            {
                throw new CommunicationException(request.ErrorMsg, ex);
            }
		}

		public override T2 Post<T1, T2>(HTTPPostRequestModel<T1> request)
		{
            try
            {
				var response = client.PostAsJsonAsync(CreatePostUri(request), request.Body).Result;
				return response.EnsureSuccessStatusCode().Content.ReadAsAsync<T2>().Result;
            }
            catch (Exception ex)
            {
                throw new CommunicationException(request.ErrorMsg, ex);
            }
		}

		public override T2 Put<T1, T2>(HTTPPutRequestModel<T1> request)
		{
            try
            {
				var response = client.PutAsJsonAsync(CreatePutUri(request), request.Body).Result;
				return response.EnsureSuccessStatusCode().Content.ReadAsAsync<T2>().Result;
            }
            catch (Exception ex)
            {
                throw new CommunicationException(request.ErrorMsg, ex);
            }

		}

		public override HTTPDeleteRequestModel Delete(HTTPDeleteRequestModel request)
		{
            try
            {
				var response = client.DeleteAsync(CreateDeleteUri(request)).Result;
				return response.EnsureSuccessStatusCode().Content.ReadAsAsync<HTTPDeleteRequestModel>().Result;
            }
            catch (Exception ex)
            {
                throw new CommunicationException(request.ErrorMsg,ex);
            }
		}

	}
}
