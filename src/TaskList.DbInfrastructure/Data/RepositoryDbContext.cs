using Microsoft.EntityFrameworkCore;
// ReSharper disable RedundantNameQualifier

namespace TaskList.DbInfrastructure.Data;

public class RepositoryDbContext : DbContext
{
#pragma warning disable CS8618 // Required by Entity Framework
    //public RepositoryDbContext(DbContextOptions options) : base(options) { }
    public RepositoryDbContext(DbContextOptions<RepositoryDbContext> options) : base(options) { }

    public DbSet<TaskList.Domain.Entities.User> Users { get; set; }
    public DbSet<TaskList.Domain.Entities.Task> Tasks { get; set; }
    public DbSet<TaskList.Domain.Entities.TaskList> TaskLists { get; set; }
    public DbSet<TaskList.Domain.Entities.TaskComment> TaskComments { get; set; }
    public DbSet<TaskList.Domain.Entities.TaskStatus> TaskStatuses { get; set; }
    public DbSet<TaskList.Domain.Entities.TaskStatusRecord> TaskStatusRecords { get; set; }
    
    
}