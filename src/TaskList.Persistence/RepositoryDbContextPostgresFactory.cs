using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TaskList.Persistence;

public sealed class RepositoryDbContextPostgresFactory : IDesignTimeDbContextFactory<RepositoryDbContext>
{
    public RepositoryDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<RepositoryDbContext>();
        var connectionString = Environment.GetEnvironmentVariable("PG_EF_MIGRATION_CONNECTION_STRING");
        optionsBuilder.UseNpgsql(connectionString);
        
        return new RepositoryDbContext(optionsBuilder.Options);
    }
}