namespace TaskList.Domain.Repositories;

public interface IRepositoryManager
{
    public IUserRepository UserRepository { get; }
    public ITaskRepository TaskRepository { get; }
    public ITaskListRepository TaskListRepository { get; }
    public ITaskCommentRepository TaskCommentRepository { get; }
    public ITaskStatusRepository TaskStatusRepository { get; }
    public ITaskStatusRecordRepository TaskStatusRecordRepository { get; }

    public IUnitOfWork UnitOfWork { get; }
}