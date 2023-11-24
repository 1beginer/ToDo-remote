using Microsoft.EntityFrameworkCore;
using ToDo.Api.Context;
using ToDo.Api.Context.Models;

namespace ToDo.Api.Repository
{
    //TODO的仓储
    public class TodoERepository : Repository<ToDoE>, IRepository<ToDoE>
    {
        public TodoERepository(MyToDoContext dbContext) : base(dbContext)
        {
        }
    }

}
