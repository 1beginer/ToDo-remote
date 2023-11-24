using ToDo.Shared.Dtos;

namespace ToDo.Api.Service
{
    public interface ILoginService
    {
        Task<ApiResponse> LoginAsync(string Account, string Password);

        Task<ApiResponse> ResgiterAsync(UserDto user);
    }
}
