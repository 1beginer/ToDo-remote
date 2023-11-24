using Microsoft.EntityFrameworkCore;
using ToDo.Api.Context;
using ToDo.Api.Context.Models;

namespace ToDo.Api.Repository
{
    public class UserRepository : Repository<User>, IRepository<User>
    {
        public UserRepository(MyToDoContext dbContext) : base(dbContext)
        {
        }
    }
}
