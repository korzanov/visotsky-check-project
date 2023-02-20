using MediatR;
using TaskList.Contracts.Responses;

namespace TaskList.Contracts.Queries;

public record QueryTaskListGetAll() : IRequest<IEnumerable<ResponseTaskList>>;