using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Floorball.REST.RequestModels;

namespace FloorballAdminiOS.Interactor.Search
{
    public class SearchInteractor<T> : Interactor
    {
		public async Task<List<T>> GetEntities(string url, string errorMsg)
		{
			return await Network.GetAsync<List<T>>(new HTTPGetRequestModel()
			{
				Url = url,
				ErrorMsg = errorMsg
			});
		}
    }
}
