using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using ToDo.Api.Service;
using ToDo.Shared.Dtos;
using ToDo.Shared.Parameters;

namespace ToDo.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MemoController : ControllerBase
    {
        private readonly IMemoService service;

        public MemoController(IMemoService service)
        {
            this.service = service;
        }
        [HttpGet]
        public async Task<ApiResponse> GetById(int Id) => await service.GetSingleAsync(Id);

        [HttpGet]
        public async Task<ApiResponse> GetAll() => await service.GetAllAsync();

        [HttpPost]
        public async Task<ApiResponse> GetPageListAsync([FromQuery] QueryParameter parameter) => await service.GetPageListAsync(parameter);

        [HttpPost]
        public async Task<ApiResponse> Update([FromQuery] MemoDto model) => await service.UpdateAsync(model);

        [HttpPost]
        public async Task<ApiResponse> Add([FromQuery] MemoDto model) => await service.AddAsync(model);

        [HttpDelete]
        public async Task<ApiResponse> Delete(int id) => await service.DeleteAsync(id);
    }
}
