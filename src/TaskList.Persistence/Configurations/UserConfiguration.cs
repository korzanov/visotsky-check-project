using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskList.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<TaskList.Domain.Entities.User>
{
    public void Configure(EntityTypeBuilder<TaskList.Domain.Entities.User> builder)
    {
        builder.ToTable(nameof(TaskList.Domain.Entities.User));
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();

        throw new NotImplementedException();
    }
}