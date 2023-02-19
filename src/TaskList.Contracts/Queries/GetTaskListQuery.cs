using MediatR;
using TaskList.Contracts.Responses;

namespace TaskList.Contracts.Queries;

public record GetTaskListQuery(Guid TaskListId) : IRequest<TaskListResponse>;