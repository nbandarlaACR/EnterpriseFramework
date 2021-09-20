using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Enterprise.Framework.GenericLib
{
    public static class RestHelper
    {
        public static IRestResponse<List<T>> Execute<T>(RestRequest request, Uri baseUrl) where T : new()
        {
            var client = new RestClient(baseUrl);
            var response = client.Execute<List<T>>(request);
            return response;
        }

        public static IRestResponse Execute(RestRequest request, Uri baseUrl)
        {
            var client = new RestClient(baseUrl);
            var response = client.Execute(request);
            return response;
        }

        public async static Task<IRestResponse> PostAsync(RestRequest request, Uri baseUrl)
        {
            var client = new RestClient(baseUrl);
            return await client.ExecuteAsync(request);

        }
    }
}
