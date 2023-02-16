namespace TaskList.Domain.Repositories;

public interface IUserRepository : IRepositoryBase<Entities.User>
{ }

public interface ITaskRepository : IRepositoryBase<Entities.Task>
{ }

public interface ITaskListRepository : IRepositoryBase<Entities.TaskList>
{ }

public interface ITaskCommentRepository : IRepositoryBase<Entities.TaskComment>
{ }

public interface ITaskStatusRepository : IRepositoryBase<Entities.TaskStatus>
{ }

public interface ITaskStatusRecordRepository : IRepositoryBase<Entities.TaskStatusRecord>
{ }