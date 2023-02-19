using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TaskList.DbInfrastructure.Identity;

/// <summary>
/// dotnet ef migrations add InitialIdentityModel --context TaskListIdentityDbContext -o Identity/PostgresMigrations
/// </summary>
public class TaskListIdentityDbContextPgFactory : IDesignTimeDbContextFactory<TaskListIdentityDbContext>
{
    public TaskListIdentityDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<TaskListIdentityDbContext>();
        var connectionString =
            "Host=127.0.0.1;Port=5432;Database=task_list_db;Username=postgres_user;Password=postgres_pwd";
            //Environment.GetEnvironmentVariable("PG_EF_MIGRATION_CONNECTION_STRING");
        optionsBuilder.UseNpgsql(connectionString);
        return new TaskListIdentityDbContext(optionsBuilder.Options);
    }
}