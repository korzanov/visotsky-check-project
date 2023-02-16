using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskList.Persistence.Configurations;

public class TaskStatusConfiguration : IEntityTypeConfiguration<TaskList.Domain.Entities.TaskStatus>
{
    public void Configure(EntityTypeBuilder<TaskList.Domain.Entities.TaskStatus> builder)
    {
        builder.ToTable(nameof(TaskList.Domain.Entities.TaskStatus));
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        
        throw new NotImplementedException();
    }
}