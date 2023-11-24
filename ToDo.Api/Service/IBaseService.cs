using ToDo.Shared.Parameters;

namespace ToDo.Api.Service
{
    public interface IBaseService<TEntity>
    {
        Task<ApiResponse> GetAllAsync();

        Task<ApiResponse> GetSingleAsync(int id);

        Task<ApiResponse> AddAsync(TEntity model);

        Task<ApiResponse> UpdateAsync(TEntity model);

        Task<ApiResponse> DeleteAsync(int id);

        Task<ApiResponse> GetPageListAsync(QueryParameter parameter);
    }
}
