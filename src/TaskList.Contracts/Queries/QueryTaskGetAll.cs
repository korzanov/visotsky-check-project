using MediatR;
using TaskList.Contracts.Responses;

namespace TaskList.Contracts.Queries;

public record QueryTaskGetAll() : IRequest<IEnumerable<ResponseTask>>;