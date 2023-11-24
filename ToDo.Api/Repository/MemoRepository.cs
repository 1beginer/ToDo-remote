using Microsoft.EntityFrameworkCore;
using ToDo.Api.Context;
using ToDo.Api.Context.Models;

namespace ToDo.Api.Repository
{
    //Memo的仓储
    public class MemoRepository : Repository<Memo>, IRepository<Memo>
    {

        public MemoRepository(MyToDoContext dbContext) : base(dbContext)
        {
        }
    }

}
