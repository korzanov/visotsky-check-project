using TaskList.Domain.Repositories;

namespace TaskList.Persistence.Repositories;

internal class UnitOfWork : RepositoryBase, IUnitOfWork
{
    public UnitOfWork(RepositoryDbContext repositoryDbContext) : base(repositoryDbContext)
    { }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return Context.SaveChangesAsync(cancellationToken);
    }
}