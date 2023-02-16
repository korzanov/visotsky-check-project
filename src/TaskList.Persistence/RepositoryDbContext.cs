using Microsoft.EntityFrameworkCore;
using TaskList.Domain.Repositories;

namespace TaskList.Persistence;

public class RepositoryDbContext : DbContext, IUnitOfWork
{
    public RepositoryDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<TaskList.Domain.Entities.User> Users { get; set; }
    public DbSet<TaskList.Domain.Entities.Task> Tasks { get; set; }
    public DbSet<TaskList.Domain.Entities.TaskList> TaskLists { get; set; }
    public DbSet<TaskList.Domain.Entities.TaskComment> TaskComments { get; set; }
    public DbSet<TaskList.Domain.Entities.TaskStatus> TaskStatuses { get; set; }
    public DbSet<TaskList.Domain.Entities.TaskStatusRecord> TaskStatusRecords { get; set; }
}