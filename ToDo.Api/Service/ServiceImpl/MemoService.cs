using AutoMapper;
using ToDo.Api.Context.Models;
using ToDo.Shared.Dtos;
using ToDo.Shared.Parameters;

namespace ToDo.Api.Service.ServiceImpl
{
    /// <summary>
    /// 备忘录的实现
    /// </summary>
    public class MemoService : IMemoService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public MemoService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        //添加
        public async Task<ApiResponse> AddAsync(MemoDto model)
        {
            try
            {
                var dbmemo = mapper.Map<Memo>(model);
                var repository = unitOfWork.GetRepository<Memo>();
                await repository.InsertAsync(dbmemo);
                if (await unitOfWork.SaveChangesAsync() > 0)
                    return new ApiResponse(true, model);
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
                var repository = unitOfWork.GetRepository<Memo>();
                var memo = await repository.GetFirstOrDefaultAsync(predicate: m => m.Id.Equals(id));
                repository.Delete(memo);
                if (await unitOfWork.SaveChangesAsync() > 0)
                    return new ApiResponse(true, "");
                return new ApiResponse("删除数据失败");
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }
        //批量查询
        public async Task<ApiResponse> GetAllAsync()
        {
            try
            {
                var repository = unitOfWork.GetRepository<Memo>();
                var memos = await repository.GetAllAsync();
                return new ApiResponse(true, memos);
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
                var repository = unitOfWork.GetRepository<Memo>();
                var memo = await repository.GetFirstOrDefaultAsync(predicate: m => m.Id.Equals(id));
                return new ApiResponse(true, memo);
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }
        //更新
        public async Task<ApiResponse> UpdateAsync(MemoDto model)
        {
            try
            {
                var dbmemo = mapper.Map<Memo>(model);
                var repository = unitOfWork.GetRepository<Memo>();
                var result = await repository.GetFirstOrDefaultAsync(predicate: t => t.Id.Equals(dbmemo.Id));

                result.Title = dbmemo.Title;
                result.Content = dbmemo.Content;
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
        //分页查询
        public async Task<ApiResponse> GetPageListAsync(QueryParameter parameter)
        {
            try
            {
                var repository = unitOfWork.GetRepository<Memo>();
                var memos = await repository.GetPagedListAsync(predicate:
                    m => string.IsNullOrWhiteSpace(parameter.Search) ? true : m.Title.Equals(parameter.Search),
                    pageIndex: parameter.PageIndex,
                    pageSize: parameter.PageSize,
                    orderBy: source => source.OrderByDescending(m => m.CreateTime));
                return new ApiResponse(true, memos);
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }
    }
}
