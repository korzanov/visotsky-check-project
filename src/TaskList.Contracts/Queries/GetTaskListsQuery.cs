using MediatR;
using TaskList.Contracts.Responses;

namespace TaskList.Contracts.Queries;

public record GetTaskListsQuery() : IRequest<IEnumerable<TaskListResponse>>;