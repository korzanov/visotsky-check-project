using MediatR;
using TaskList.Contracts.Responses;

namespace TaskList.Contracts.Commands;

public record UpdateUserCommand(string Login, string Name, string Email) : IRequest<UserResponse>;