using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Shared;
using ToDo.Shared.Contact;
using ToDo.Shared.Parameters;

namespace ToDo.Services
{
    /*这里的泛型TEntity必须是一个类类型classType。这意味着只能将类型作为参数传递给这个接口的方法*/
    public interface IBaseService<TEntity> where TEntity : class
    {
        Task<ApiResponseShared<TEntity>> AddAsync(TEntity entity);

        Task<ApiResponseShared<TEntity>> UpdateAsync(TEntity entity);

        Task<ApiResponseShared> DeletedAsync(int id);

        Task<ApiResponseShared<TEntity>> GetFirstOfDefaultAsync(int id);

        Task<ApiResponseShared<PagedList<TEntity>>> GetPageListAsync(QueryParameter parameter);
    }
}
