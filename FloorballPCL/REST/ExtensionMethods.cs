using System;
using System.Collections.Generic;
using System.Text;

namespace FloorballPCL.REST
{
    public static class ExtensionMethods
    {
		public static UriBuilder AddQueryParams(this UriBuilder builder, Dictionary<string, string> queryParams)
		{

            if (queryParams != null) 
            {
				StringBuilder query = new StringBuilder();

				foreach (var param in queryParams)
				{
					query = query.Append("&").Append(param.Key).Append("=").Append(param.Value);
				}

				if (queryParams.Count > 0)
				{
					builder.Query = query.Remove(0, 1).Insert(0, "?").ToString();
				}    
            }

			return builder;
		}

        public static UriBuilder AddPathParams(this UriBuilder builder, Dictionary<string,string> pathParams) 
        {

            if (pathParams != null) 
            {
				foreach (var param in pathParams)
				{
					builder.Path.Replace("{" + param.Key + "}", param.Value);
				}   
            }

            return builder;
        }

    }
}
