using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDo.Api.Service;
using ToDo.Shared.Dtos;

namespace ToDo.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService service;

        public LoginController(ILoginService service)
        {
            this.service = service;
        }
        [HttpPost]
        public async Task<ApiResponse> Login([FromBody] UserDto param) => await service.LoginAsync(param.Account, param.Password);

        [HttpPost]
        public async Task<ApiResponse> Resgiter([FromBody] UserDto user) => await service.ResgiterAsync(user);
    }
}
