using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskList.DbInfrastructure.Data.Configurations;

public class TaskStatusRecordConfiguration : IEntityTypeConfiguration<TaskList.Domain.Entities.TaskStatusRecord>
{
    public void Configure(EntityTypeBuilder<TaskList.Domain.Entities.TaskStatusRecord> builder)
    {
        builder.ToTable(nameof(TaskList.Domain.Entities.TaskStatusRecord));
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        
        throw new NotImplementedException();
    }
}