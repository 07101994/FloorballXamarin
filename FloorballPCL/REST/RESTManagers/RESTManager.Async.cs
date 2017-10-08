using System;
using System.Net.Http;
using System.Threading.Tasks;
using Floorball.Exceptions;
using Floorball.REST.RequestModels;
using Newtonsoft.Json;

namespace Floorball.REST.RESTManagers
{
    public partial class RESTManager : RESTManagerBase
    {

        public override async Task<T> GetAsync<T>(HTTPGetRequestModel request)
        {
            try
            {
                var req = CreateGetUri(request);

				var response = await client.GetAsync(CreateGetUri(request));
                var contentString = await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(contentString);

				//return await response.EnsureSuccessStatusCode().Content.ReadAsAsync<T>();
            }
            catch (Exception ex)
            {
                throw new CommunicationException(request.ErrorMsg, ex);
            }

        }

        public override async Task<T2> PostAsync<T1, T2>(HTTPPostRequestModel<T1> request) 
        {
            try
            {
				var response = await client.PostAsJsonAsync(CreatePostUri(request), request.Body);
                var contentString = await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T2>(contentString);
            }
            catch (Exception ex)
            {
                throw new CommunicationException(request.ErrorMsg, ex);
            }
        }

		public override async Task<T2> PutAsync<T1, T2>(HTTPPutRequestModel<T1> request)
		{
            try
            {
				var response = await client.PutAsJsonAsync(CreatePutUri(request), request.Body);
                var contentString = await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T2>(contentString);

				//return await response.EnsureSuccessStatusCode().Content.ReadAsAsync<T2>();
            }
            catch (Exception ex)
            {
                throw new CommunicationException(request.ErrorMsg, ex);
            }

		}

		public override async Task<HTTPDeleteRequestModel> DeleteAsync(HTTPDeleteRequestModel request)
		{
            try
            {
				var response = await client.DeleteAsync(CreateDeleteUri(request));
                var contentString = await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<HTTPDeleteRequestModel>(contentString);

				//return await response.EnsureSuccessStatusCode().Content.ReadAsAsync<HTTPDeleteRequestModel>();
            }
            catch (Exception ex)
            {
                throw new CommunicationException(request.ErrorMsg, ex);
            }
		}
    }
}
