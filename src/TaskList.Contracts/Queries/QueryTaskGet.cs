using MediatR;
using TaskList.Contracts.Responses;

namespace TaskList.Contracts.Queries;

public record QueryTaskGet(Guid Id) : IRequest<ResponseTask>;