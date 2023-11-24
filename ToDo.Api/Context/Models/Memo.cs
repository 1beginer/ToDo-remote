namespace ToDo.Api.Context.Models
{
    public class Memo : BaseEntity
    {
        public string Title { get; set; }
        public string? Content { get; set; }
    }
}
