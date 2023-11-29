using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Shared;
using ToDo.Shared.Dtos;

namespace ToDo.Services
{
    public interface ILoginService
    {
        Task<ApiResponseShared<UserDto>> LoginAsync(UserDto userDto);
        Task<ApiResponseShared> ResgiterAsync(UserDto userDto);
    }
}
