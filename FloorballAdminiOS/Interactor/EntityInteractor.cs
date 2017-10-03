using System;
using System.Threading.Tasks;
using Floorball.REST.RequestModels;

namespace FloorballAdminiOS.Interactor
{
    public class EntityInteractor<T> : Interactor
    {

        public async Task<int> AddEntity(String url, string errorMsg, T body)
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

    }
}
