using Microsoft.EntityFrameworkCore;
using TaskList.Persistence;

namespace TaskList.Presentation.StartUp;

internal static class DataBaseStartUp
{
    internal static async Task ApplyMigrations(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        await using var dbContext = scope.ServiceProvider.GetRequiredService<RepositoryDbContext>();
        await dbContext.Database.EnsureCreatedAsync();
        await dbContext.Database.MigrateAsync();
    }

    internal static void SetConnectionString(DbContextOptionsBuilder dbContextOptionsBuilder)
    {
#if RELEASE
        var connectionString = Environment.GetEnvironmentVariable("dbConnectionString");
#else
        const string connectionString = "Host=127.0.0.1;Port=5432;Database=task_list_db;Username=postgres_user;Password=postgres_pwd";
#endif
        dbContextOptionsBuilder.UseNpgsql(connectionString);
    }
}