using System;
using System.Threading.Tasks;
using Floorball.REST.RequestModels;

namespace Floorball.REST.RESTManagers
{
    public interface IRESTManager
    {
		 HTTPDeleteRequestModel Delete(HTTPDeleteRequestModel request);
		 Task<HTTPDeleteRequestModel> DeleteAsync(HTTPDeleteRequestModel request);

		 T Get<T>(HTTPGetRequestModel request);
		 Task<T> GetAsync<T>(HTTPGetRequestModel request);

		 T2 Post<T1, T2>(HTTPPostRequestModel<T1> request);
		 Task<T2> PostAsync<T1, T2>(HTTPPostRequestModel<T1> request);

		 T2 Put<T1, T2>(HTTPPutRequestModel<T1> request);
		 Task<T2> PutAsync<T1, T2>(HTTPPutRequestModel<T1> request);

    }
}
