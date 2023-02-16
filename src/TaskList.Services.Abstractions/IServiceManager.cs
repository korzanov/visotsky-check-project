namespace TaskList.Services.Abstractions;

public interface IServiceManager
{
    public IUserService UserService { get; }

    public ITaskService TaskService { get; }

    public ITaskListService TaskListService { get; }

    public ITaskCommentService TaskCommentService { get; }
}