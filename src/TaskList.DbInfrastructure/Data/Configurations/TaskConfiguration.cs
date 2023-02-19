using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskList.DbInfrastructure.Data.Configurations;

public class TaskConfiguration : IEntityTypeConfiguration<TaskList.Domain.Entities.Task>
{
    public void Configure(EntityTypeBuilder<TaskList.Domain.Entities.Task> builder)
    {
        builder.ToTable(nameof(TaskList.Domain.Entities.Task));
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        
        throw new NotImplementedException();
    }
}