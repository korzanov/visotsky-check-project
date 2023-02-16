using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskList.Persistence.Configurations;

public class TaskCommentConfiguration : IEntityTypeConfiguration<TaskList.Domain.Entities.TaskComment>
{
    public void Configure(EntityTypeBuilder<TaskList.Domain.Entities.TaskComment> builder)
    {
        builder.ToTable(nameof(TaskList.Domain.Entities.TaskComment));
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        
        throw new NotImplementedException();
    }
}