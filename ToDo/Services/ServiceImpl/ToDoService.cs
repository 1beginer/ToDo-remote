using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Shared;
using ToDo.Shared.Contact;
using ToDo.Shared.Dtos;
using ToDo.Shared.Parameters;

namespace ToDo.Services.ServiceImpl
{
    public class ToDoService : BaseService<ToDoDto>, IToDoService
    {
        private readonly HttpRestClient client;

        public ToDoService(HttpRestClient client) : base(client, "ToDo")
        {
            this.client = client;
        }

        public async Task<ApiResponseShared<PagedList<ToDoDto>>> GetAllFilterAsync(ToDoParameter parameter)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Post;
            request.Route = $"api/ToDo/GetAllFilter?pageIndex={parameter.PageIndex}" +
                $"&pageSize={parameter.PageSize}" +
                $"&search={parameter.Search}" +
                $"&status={parameter.Status}";
            return await client.ExecuteAsyncx<PagedList<ToDoDto>>(request);
        }

        public async Task<ApiResponseShared<SummeryDto>> SummaryAsync()
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Get;
            request.Route = "api/ToDo/Summary";
            return await client.ExecuteAsyncx<SummeryDto>(request);
        }
    }
}
