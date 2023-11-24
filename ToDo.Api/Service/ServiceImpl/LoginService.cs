using AutoMapper;
using ToDo.Api.Context.Models;
using ToDo.Shared.Dtos;

namespace ToDo.Api.Service.ServiceImpl
{
    public class LoginService : ILoginService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public LoginService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ApiResponse> LoginAsync(string Account, string Password)
        {
            try
            {
                var model = await unitOfWork.GetRepository<User>().GetFirstOrDefaultAsync(predicate:
                    x => (x.Account.Equals(Account)) &&
                    (x.Password.Equals(Password)));
                if (model == null)
                    return new ApiResponse("账号或密码错误，请重试！");
                return new ApiResponse(true, model);
            }
            catch (Exception)
            {
                return new ApiResponse("登录失败");
            }
        }

        public async Task<ApiResponse> ResgiterAsync(UserDto user)
        {
            try
            {
                var model = mapper.Map<User>(user);
                var repository = unitOfWork.GetRepository<User>();
                var userModel = await repository.GetFirstOrDefaultAsync(predicate: x => x.Account.Equals(model.Account));
                if (userModel != null)
                    return new ApiResponse($"当前账号：{model.Account}已存在，请重新注册！");
                model.CreateTime = DateTime.Now;
                await repository.InsertAsync(model);
                if (await unitOfWork.SaveChangesAsync() > 0)
                    return new ApiResponse(true, model);
                return new ApiResponse("注册失败，请稍后重试");
            }
            catch (Exception ex)
            {
                return new ApiResponse("注册账号失败！");
            }
        }
    }
}
