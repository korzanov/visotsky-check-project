using MediatR;
using TaskList.Contracts.Responses;

namespace TaskList.Contracts.Commands;

public record CommandTaskListUpdate(Guid Id, string Name, string Description) : IRequest<TaskListResponse>;