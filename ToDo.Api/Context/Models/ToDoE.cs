namespace ToDo.Api.Context.Models
{
    public class ToDoE : BaseEntity
    {
        public string Title { get; set; }
        public string? Content { get; set; }
        public int Status { get; set; }
    }
}
