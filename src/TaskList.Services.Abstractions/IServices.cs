using TaskList.Contracts;

namespace TaskList.Services.Abstractions;

public interface IUserService : ICrudService<UserDto, UserCreateDto, UserUpdateDto> 
{ }

public interface ITaskService : ICrudService<TaskDto, TaskCreateDto, TaskUpdateDto>
{ }

public interface ITaskListService : ICrudService<TaskListDto, TaskListCreateDto, TaskListUpdateDto> 
{ }

public interface ITaskCommentService : ICrudService<TaskCommentDto, TaskCommentCreateDto, TaskCommentUpdateDto> 
{ }