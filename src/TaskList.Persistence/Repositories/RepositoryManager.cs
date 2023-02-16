using Microsoft.EntityFrameworkCore;
using TaskList.Domain.Repositories;

namespace TaskList.Persistence.Repositories;

public class RepositoryManager : IRepositoryManager
{
    private readonly Lazy<IUserRepository> _lazyUserRepository;
    private readonly Lazy<ITaskRepository> _lazyTaskRepository;
    private readonly Lazy<ITaskListRepository> _lazyTaskListRepository;
    private readonly Lazy<ITaskCommentRepository> _lazyTaskCommentRepository;
    private readonly Lazy<ITaskStatusRepository> _lazyTaskStatusRepository;
    private readonly Lazy<ITaskStatusRecordRepository> _lazyTaskStatusRecordRepository;

    public RepositoryManager(RepositoryDbContext repositoryDbContext)
    {
        _lazyUserRepository = new Lazy<IUserRepository>();
        _lazyTaskRepository = new Lazy<ITaskRepository>();
        _lazyTaskListRepository = new Lazy<ITaskListRepository>();
        _lazyTaskCommentRepository = new Lazy<ITaskCommentRepository>();
        _lazyTaskStatusRepository = new Lazy<ITaskStatusRepository>();
        _lazyTaskStatusRecordRepository = new Lazy<ITaskStatusRecordRepository>();
        
        UnitOfWork = repositoryDbContext;
    }

    public IUserRepository UserRepository => _lazyUserRepository.Value;
    public ITaskRepository TaskRepository => _lazyTaskRepository.Value;
    public ITaskListRepository TaskListRepository => _lazyTaskListRepository.Value;
    public ITaskCommentRepository TaskCommentRepository => _lazyTaskCommentRepository.Value;
    public ITaskStatusRepository TaskStatusRepository => _lazyTaskStatusRepository.Value;
    public ITaskStatusRecordRepository TaskStatusRecordRepository => _lazyTaskStatusRecordRepository.Value;
    public IUnitOfWork UnitOfWork { get; }
}