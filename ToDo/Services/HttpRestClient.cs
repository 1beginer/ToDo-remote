using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Shared;

namespace ToDo.Services
{
    public class HttpRestClient
    {
        private readonly string webUrl;
        protected readonly RestClient client;
        protected readonly RestClientOptions options;
        public HttpRestClient(string webUrl)
        {
            this.webUrl = webUrl;
            options = new RestClientOptions(webUrl) { MaxTimeout = -1 };
            client = new RestClient(options);
        }

        public async Task<ApiResponseShared> ExecuteAsyncx(BaseRequest baseRequest)
        {
            var request = new RestRequest(baseRequest.Route, baseRequest.Method);
            request.AddHeader("Content-Type", baseRequest.ContentType);

            if (baseRequest.Parameter != null)
                request.AddParameter("param", JsonConvert.SerializeObject(baseRequest.Parameter), ParameterType.RequestBody);

            var response = await client.ExecuteAsync(request);

            return JsonConvert.DeserializeObject<ApiResponseShared>(response.Content);
        }


        public async Task<ApiResponseShared<T>> ExecuteAsyncx<T>(BaseRequest baseRequest)
        {
            var request = new RestRequest(baseRequest.Route, baseRequest.Method);
            request.AddHeader("Content-Type", baseRequest.ContentType);

            if (baseRequest.Parameter != null)
                request.AddParameter("param", JsonConvert.SerializeObject(baseRequest.Parameter), ParameterType.RequestBody);


            var response = await client.ExecuteAsync(request);

            return JsonConvert.DeserializeObject<ApiResponseShared<T>>(response.Content);
        }
    }
}
