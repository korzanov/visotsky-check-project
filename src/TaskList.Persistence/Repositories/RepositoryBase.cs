namespace TaskList.Persistence.Repositories;

internal abstract class RepositoryBase
{
    protected readonly RepositoryDbContext Context;

    protected RepositoryBase(RepositoryDbContext context)
    {
        Context = context;
    }
}