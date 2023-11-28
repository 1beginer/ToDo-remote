using AutoMapper;
using System.Reflection.Metadata;
using ToDo.Api.Context.Models;
using ToDo.Shared.Dtos;
using System.Collections.ObjectModel;
using ToDo.Shared.Parameters;

namespace ToDo.Api.Service.ServiceImpl
{
    /// <summary>
    /// 待办事项的实现
    /// </summary>
    public class ToDoService : IToDoService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ToDoService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        //添加
        public async Task<ApiResponse> AddAsync(ToDoDto model)
        {
            try
            {
                var todo = mapper.Map<ToDoE>(model);
                var repository = unitOfWork.GetRepository<ToDoE>();
                await repository.InsertAsync(todo);
                if (await unitOfWork.SaveChangesAsync() > 0)
                    return new ApiResponse(true, todo);
                return new ApiResponse("添加数据失败");
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }

        }
        //删除
        public async Task<ApiResponse> DeleteAsync(int id)
        {
            try
            {
                var repository = unitOfWork.GetRepository<ToDoE>();
                var todo = await repository.GetFirstOrDefaultAsync(predicate: t => t.Id.Equals(id));
                repository.Delete(todo);
                if (await unitOfWork.SaveChangesAsync() > 0)
                    return new ApiResponse(true, "");
                return new ApiResponse("删除数据失败");
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }
        //整体查询
        public async Task<ApiResponse> GetAllAsync()
        {
            try
            {
                var repository = unitOfWork.GetRepository<ToDoE>();
                var todos = await repository.GetAllAsync();
                return new ApiResponse(true, todos);
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }
        //查询
        public async Task<ApiResponse> GetSingleAsync(int id)
        {
            try
            {
                var repository = unitOfWork.GetRepository<ToDoE>();
                var todo = await repository.GetFirstOrDefaultAsync(predicate: t => t.Id.Equals(id));
                return new ApiResponse(true, todo);
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }
        //更新
        public async Task<ApiResponse> UpdateAsync(ToDoDto model)
        {
            try
            {
                var dbtodo = mapper.Map<ToDoE>(model);
                var repository = unitOfWork.GetRepository<ToDoE>();
                var result = await repository.GetFirstOrDefaultAsync(predicate: t => t.Id.Equals(dbtodo.Id));

                result.Title = dbtodo.Title;
                result.Content = dbtodo.Content;
                result.Status = dbtodo.Status;
                repository.Update(result);
                if (await unitOfWork.SaveChangesAsync() > 0)
                    return new ApiResponse(true, result);
                return new ApiResponse("更新数据异常！");
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> GetPageListAsync(QueryParameter parameter)
        {
            try
            {
                var repository = unitOfWork.GetRepository<ToDoE>();
                var todos = await repository.GetPagedListAsync(predicate:
                    t => string.IsNullOrWhiteSpace(parameter.Search) ? true : t.Title.Contains(parameter.Search),
                    pageIndex: parameter.PageIndex,
                    pageSize: parameter.PageSize,
                    orderBy: source => source.OrderByDescending(t => t.CreateTime));
                return new ApiResponse(true, todos);
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        public async Task<ApiResponse> GetAllFilterAsync(ToDoParameter parameter)
        {
            try
            {
                var repository = unitOfWork.GetRepository<ToDoE>();
                var todos = await repository.GetPagedListAsync(predicate:
                t => (string.IsNullOrWhiteSpace(parameter.Search) ? true : t.Title.Contains(parameter.Search))
                   && (parameter.Status == null ? true : t.Status.Equals(parameter.Status)),
                   pageIndex: parameter.PageIndex,
                   pageSize: parameter.PageSize,
                   orderBy: source => source.OrderByDescending(t => t.CreateTime));
                return new ApiResponse(true, todos);
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }
        /// <summary>
        /// 汇总方法
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResponse> Summary()
        {
            try
            {
                //待办事项结果
                var todos = await unitOfWork.GetRepository<ToDoE>().GetAllAsync(
                    orderBy: source => source.OrderByDescending(t => t.CreateTime));
                //备忘录结果
                var memos = await unitOfWork.GetRepository<Memo>().GetAllAsync(
                    orderBy: source => source.OrderByDescending(t => t.CreateTime));

                SummeryDto summery = new SummeryDto();
                summery.Sum = todos.Count();//汇总待办数量
                summery.CompletedCount = todos.Where(t => t.Status == 1).Count();//统计完成数量
                summery.CompletedRatio = (summery.CompletedCount / (double)summery.Sum).ToString("0%");//计算完成比例
                summery.MemoCount = memos.Count();//统计备忘录数量

                summery.ToDoList = new ObservableCollection<ToDoDto>(mapper.Map<List<ToDoDto>>(todos.Where(t => t.Status == 0)));//统计待办事件集合
                summery.MemoList = new ObservableCollection<MemoDto>(mapper.Map<List<MemoDto>>(memos));//统计备忘事件集合

                return new ApiResponse(true, summery);
            }
            catch (Exception ex)
            {
                return new ApiResponse(false, "出现异常");
            }
        }
    }
}
