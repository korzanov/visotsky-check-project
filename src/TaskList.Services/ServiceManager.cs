using TaskList.Domain.Repositories;
using TaskList.Services.Abstractions;

namespace TaskList.Services;

public sealed class ServiceManager : IServiceManager
{
    private readonly Lazy<IUserService> _lazyUserService;
    private readonly Lazy<ITaskService> _lazyTaskService;
    private readonly Lazy<ITaskListService> _lazyTaskListService;
    private readonly Lazy<ITaskCommentService> _lazyTaskCommentService;

    public ServiceManager(IRepositoryManager repositoryManager)
    {
        _lazyUserService = new Lazy<IUserService>(() => new UserService(repositoryManager));
        _lazyTaskService = new Lazy<ITaskService>(() => new TaskService(repositoryManager));
        _lazyTaskListService = new Lazy<ITaskListService>(() => new TaskListService(repositoryManager));
        _lazyTaskCommentService = new Lazy<ITaskCommentService>(() => new TaskCommentService(repositoryManager));
    }

    public IUserService UserService => _lazyUserService.Value;
    public ITaskService TaskService => _lazyTaskService.Value;
    public ITaskListService TaskListService => _lazyTaskListService.Value;
    public ITaskCommentService TaskCommentService => _lazyTaskCommentService.Value;
}