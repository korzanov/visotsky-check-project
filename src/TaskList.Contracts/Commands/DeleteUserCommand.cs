using MediatR;

namespace TaskList.Contracts.Commands;

public record DeleteUserCommand(string UserName) : IRequest;