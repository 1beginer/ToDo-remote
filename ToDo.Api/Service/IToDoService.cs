using ToDo.Api.Context.Models;
using ToDo.Shared.Dtos;
using ToDo.Shared.Parameters;

namespace ToDo.Api.Service
{
    public interface IToDoService : IBaseService<ToDoDto>
    {
        Task<ApiResponse> GetAllFilterAsync(ToDoParameter query);
    }
}
