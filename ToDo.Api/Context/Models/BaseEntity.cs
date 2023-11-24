namespace ToDo.Api.Context.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreateTime { get; set; }
        public int Status { get; set; }
    }
}
