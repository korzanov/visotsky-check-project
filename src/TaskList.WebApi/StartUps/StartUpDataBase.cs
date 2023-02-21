using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskList.Contracts.Commands;
using TaskList.DbInfrastructure.Data;
using TaskList.DbInfrastructure.Identity;

namespace TaskList.WebApi.StartUps;

internal static class StartUpDataBase
{
    internal static async Task SeedDefaultStatuses(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        await mediator.Send(new CommandTaskStatusSetDefaults());
    }
    
    internal static async Task ApplyIdentityMigrations(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        await using var dbContext = scope.ServiceProvider.GetRequiredService<TaskListIdentityDbContext>();
        await dbContext.Database.EnsureCreatedAsync();
        await dbContext.Database.MigrateAsync();
    }
    internal static async Task ApplyRepositoryMigrations(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        await using var dbContext = scope.ServiceProvider.GetRequiredService<RepositoryDbContext>();
        await dbContext.Database.EnsureCreatedAsync();
        await dbContext.Database.MigrateAsync();
    }

    internal static void SetConnectionString<TDbContext>(DbContextOptionsBuilder dbContextOptionsBuilder) where TDbContext : DbContext
    {
#if RELEASE
        var connectionString = Environment.GetEnvironmentVariable("dbConnectionString");
#else
        const string connectionString = "Host=127.0.0.1;Port=5432;Database=task_list_db;Username=postgres_user;Password=postgres_pwd";
#endif
        dbContextOptionsBuilder.UseNpgsql(connectionString, x => x.MigrationsAssembly(typeof(TDbContext).Assembly.FullName));
    }
}