using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Shared;
using ToDo.Shared.Dtos;

namespace ToDo.Services.ServiceImpl
{
    public class LoginService : ILoginService
    {
        private readonly HttpRestClient client;
        private readonly string serviceName = "Login";
        public LoginService(HttpRestClient client)
        {
            this.client = client;
        }

        public async Task<ApiResponseShared<UserDto>> LoginAsync(UserDto userDto)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Post;
            request.Route = $"api/{serviceName}/Login";
            request.Parameter = userDto;
            return await client.ExecuteAsyncx<UserDto>(request);
        }

        public async Task<ApiResponseShared> ResgiterAsync(UserDto userDto)
        {
            BaseRequest request = new BaseRequest();
            request.Method = RestSharp.Method.Post;
            request.Route = $"api/{serviceName}/Resgiter";
            request.Parameter = userDto;
            return await client.ExecuteAsyncx(request);
        }
    }
}
