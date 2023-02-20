using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskList.Domain.Repositories;

namespace TaskList.DbInfrastructure.Data;

public class EfRepository<T> : RepositoryBase<T>, IRepository<T>, IRepositoryReadOnly<T> where T : class
{
    private readonly DbContext _context;
    public EfRepository(RepositoryDbContext dbContext) : base(dbContext)
    {
        _context = dbContext;
    }

    public override async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _context.ChangeTracker.Clear(); // for in memory db tests
        await base.UpdateAsync(entity, cancellationToken);
    }
}