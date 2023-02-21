using MediatR;
using TaskList.Contracts.Responses;

namespace TaskList.Contracts.Queries;

public record QueryTaskStatusGetDefault() : IRequest<ResponseTaskStatus>;