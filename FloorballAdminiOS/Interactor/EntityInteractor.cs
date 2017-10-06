using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Floorball.REST.RequestModels;

namespace FloorballAdminiOS.Interactor
{
    public class EntityInteractor<T> : Interactor
    {

        public async Task<int> AddEntity(string url, string errorMsg, T body)
        {
			return await Network.PostAsync<T, int>(new HTTPPostRequestModel<T>
			{
				Url = url,
				ErrorMsg = errorMsg,
				Body = body
			});
        }

        public int UpdateEntity()
        {
            return 1;   
        }

        public async Task<T> GetEntityById(string url, string errorMsg, string id)
        {
			return await Network.GetAsync<T>(new HTTPGetRequestModel()
			{
				Url = url,
				UrlParams = new Dictionary<string, string>() { { "id", id } },
				ErrorMsg = errorMsg
			});
        }

        public async Task<List<T1>> GetEntities<T1>(string url, string errorMsg)
        {
			return await Network.GetAsync<List<T1>>(new HTTPGetRequestModel()
			{
				Url = url,
				ErrorMsg = errorMsg
			});
        }

    }
}
