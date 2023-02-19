using MediatR;

namespace TaskList.Contracts.Commands;

public record DeleteUserCommand(Guid Id) : IRequest;