using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ToDo.Api.Context.Models.ModelConfigs
{
    public class MemoConfigs : IEntityTypeConfiguration<Memo>
    {
        public void Configure(EntityTypeBuilder<Memo> builder)
        {
            builder.ToTable("T_Memos");
            builder.Property(m => m.Title).HasMaxLength(64);
            builder.Property(m => m.Content).HasMaxLength(1024);
        }
    }
}
