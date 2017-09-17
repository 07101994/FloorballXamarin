using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Floorball.REST
{
  //  class FloorballRESTClient : RestClient
  //  {

  //      private RestRequest request;

  //      public FloorballRESTClient(string url) : base(url)
  //      {
  //          // Override with Newtonsoft JSON Handler
  //          AddHandler("application/json", FloorballSerializer.Instance);
  //          AddHandler("text/json", FloorballSerializer.Instance);
  //          AddHandler("text/x-json", FloorballSerializer.Instance);
  //          AddHandler("text/javascript", FloorballSerializer.Instance);
  //          AddHandler("*+json", FloorballSerializer.Instance);
  //      }

  //      public async Task<IRestResponse> ExecuteRequestAsync(string path, Method method, Dictionary<string, string> urlParams = null, Dictionary<string, string> queryParams = null, object body = null, Dictionary<string, string> headers = null)
  //      {
  //          RestResponse response;

  //          request = new RestRequest(path, method);
  //          request.RequestFormat = DataFormat.Json;
  //          request.JsonSerializer = FloorballSerializer.Instance;
  //          if (urlParams != null)
  //          {
  //              foreach (var urlParam in urlParams)
  //              {
  //                  request.AddUrlSegment(urlParam.Key, urlParam.Value);
  //              }
  //          }
  //          if (headers != null)
  //          {
  //              foreach (var header in headers)
  //              {
  //                  request.AddHeader(header.Key, header.Value);
  //              }
  //          }

  //          switch (method)
  //          {
  //              case Method.GET:
  //                  response = await Task.Run(() => ExecuteGET(queryParams)) as RestResponse;
  //                  break;
  //              case Method.POST:
  //                  response = await Task.Run(() => ExecutePOST(body)) as RestResponse;
  //                  break;
  //              case Method.PUT:
  //                  response = await Task.Run(() => ExecutePUT()) as RestResponse;
  //                  break;
  //              case Method.DELETE:
  //                  response = await Task.Run(() => ExecuteDELETE()) as RestResponse;
  //                  break;
  //              default:
  //                  return null;
  //          }

  //          if (response.ErrorException != null)
  //          {
  //              throw response.ErrorException;
  //          }
  //          else
  //          {
  //              if (response.StatusCode.ToString()[0] == '4')
  //              {
  //                  throw new Exception(response.Content);
  //              }
  //          }

  //          return response;
  //      }

		//public IRestResponse ExecuteRequest(string path, Method method, Dictionary<string, string> urlParams = null, Dictionary<string, string> queryParams = null, object body = null, Dictionary<string, string> headers = null)
		//{
		//	RestResponse response;

		//	request = new RestRequest(path, method);
		//	request.RequestFormat = DataFormat.Json;
		//	request.JsonSerializer = FloorballSerializer.Instance;
		//	if (urlParams != null)
		//	{
		//		foreach (var urlParam in urlParams)
		//		{
		//			request.AddUrlSegment(urlParam.Key, urlParam.Value);
		//		}
		//	}
		//	if (headers != null)
		//	{
		//		foreach (var header in headers)
		//		{
		//			request.AddHeader(header.Key, header.Value);
		//		}
		//	}

		//	switch (method)
		//	{
		//		case Method.GET:
		//			response = ExecuteGET(queryParams) as RestResponse;
		//			break;
		//		case Method.POST:
		//			response = ExecutePOST(body) as RestResponse;
		//			break;
		//		case Method.PUT:
		//			response = ExecutePUT() as RestResponse;
		//			break;
		//		case Method.DELETE:
		//			response = ExecuteDELETE() as RestResponse;
		//			break;
		//		default:
		//			return null;
		//	}

		//	if (response.ErrorException != null)
		//	{
		//		throw response.ErrorException;
		//	}
		//	else
		//	{
		//		if (response.StatusCode.ToString()[0] == '4')
		//		{
		//			throw new Exception(response.Content);
		//		}
		//	}

		//	return response;
		//}



		//private IRestResponse ExecuteGET(Dictionary<string, string> queryParams)
    //    {
    //        if (queryParams != null)
    //        {
    //            foreach (var queryParam in queryParams)
    //            {
    //                request.AddQueryParameter(queryParam.Key, queryParam.Value);
    //            }
    //        }

    //        return ExecuteAsGet(request, "GET");
    //    }

    //    private IRestResponse ExecutePOST(object body)
    //    {
    //        if (body != null)
    //        {
    //            //request.RequestFormat = DataFormat.Json;
    //            request.AddJsonBody(body);
    //        }

    //        return ExecuteAsPost(request, "POST");
    //    }

    //    private IRestResponse ExecuteDELETE()
    //    {
    //        return Execute(request);
    //    }

    //    private IRestResponse ExecutePUT()
    //    {
    //        return Execute(request);
    //    }
    //}
}
