using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskList.Persistence.Configurations;

public class TaskListConfiguration : IEntityTypeConfiguration<TaskList.Domain.Entities.TaskList>
{
    public void Configure(EntityTypeBuilder<TaskList.Domain.Entities.TaskList> builder)
    {
        builder.ToTable(nameof(TaskList.Domain.Entities.TaskList));
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        
        throw new NotImplementedException();
    }
}