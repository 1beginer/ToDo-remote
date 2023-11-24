using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ToDo.Api.Context.Models.ModelConfigs
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("T_Users");
            builder.Property(u => u.Name).HasMaxLength(64);
            builder.Property(u => u.Password).HasMaxLength(128);
        }
    }
}
