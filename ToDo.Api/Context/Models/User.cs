using System.ComponentModel.DataAnnotations;

namespace ToDo.Api.Context.Models
{
    public class User : BaseEntity
    {
        public string Account { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
