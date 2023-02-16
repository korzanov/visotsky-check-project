using TaskList.Domain.Repositories;

namespace TaskList.Services;

internal abstract class ServiceBase
{
    protected readonly IRepositoryManager RepositoryManager;
    
    protected ServiceBase(IRepositoryManager repositoryManager)
    {
        RepositoryManager = repositoryManager;
    }
}