using MediatR;
using TaskList.Contracts.Responses;

namespace TaskList.Contracts.Queries;

public record QueryTaskListGet(Guid TaskListId) : IRequest<TaskListResponse>;