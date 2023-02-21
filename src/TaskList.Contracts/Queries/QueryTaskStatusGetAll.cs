using MediatR;
using TaskList.Contracts.Responses;

namespace TaskList.Contracts.Queries;

public record QueryTaskStatusGetAll() : IRequest<IEnumerable<ResponseTaskStatus>>;