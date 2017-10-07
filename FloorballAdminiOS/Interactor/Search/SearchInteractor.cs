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

		public async Task<List<T1>> GetEntities<T1>(string url, string errorMsg)
		{
			return await Network.GetAsync<List<T1>>(new HTTPGetRequestModel()
			{
				Url = url,
				ErrorMsg = errorMsg
			});
		}

		public async Task<Dictionary<int, List<int>>> GetEntityMappings(string url, string errorMsg)
		{
			return await Network.GetAsync<Dictionary<int, List<int>>>(new HTTPGetRequestModel()
			{
				Url = url,
				ErrorMsg = errorMsg
			});
		}
    }
}
