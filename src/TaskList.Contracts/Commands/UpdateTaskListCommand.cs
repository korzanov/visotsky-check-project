using MediatR;
using TaskList.Contracts.Responses;

namespace TaskList.Contracts.Commands;

public record UpdateTaskListCommand(Guid Id, string Name, string Description) : IRequest<TaskListResponse>;