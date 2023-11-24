using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ToDo.Api.Context.Models.ModelConfigs
{
    public class ToDoEConfigs : IEntityTypeConfiguration<ToDoE>
    {
        public void Configure(EntityTypeBuilder<ToDoE> builder)
        {
            builder.ToTable("T_ToDos");
            builder.Property(t => t.Title).HasMaxLength(64);
            builder.Property(t => t.Content).HasMaxLength(1024);
        }
    }
}
