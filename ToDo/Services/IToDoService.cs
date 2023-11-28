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
    public interface IToDoService : IBaseService<ToDoDto>
    {
        Task<ApiResponseShared<PagedList<ToDoDto>>> GetAllFilterAsync(ToDoParameter parameter);

        Task<ApiResponseShared<SummeryDto>> SummaryAsync();
    }

}
