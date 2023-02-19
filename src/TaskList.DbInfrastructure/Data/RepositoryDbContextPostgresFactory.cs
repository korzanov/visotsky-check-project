using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TaskList.DbInfrastructure.Data;

namespace TaskList.DbInfrastructure.Data;

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