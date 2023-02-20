using MediatR;
using TaskList.Contracts.Responses;

namespace TaskList.Contracts.Commands;

public record CommandTaskUpdate(Guid Id, string Name, string Description) : IRequest<ResponseTask>;