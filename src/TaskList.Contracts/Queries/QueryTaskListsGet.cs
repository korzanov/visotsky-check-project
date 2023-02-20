using MediatR;
using TaskList.Contracts.Responses;

namespace TaskList.Contracts.Queries;

public record QueryTaskListsGet() : IRequest<IEnumerable<ResponseTaskList>>;