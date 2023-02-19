using MediatR;
using TaskList.Contracts.Responses;

namespace TaskList.Contracts.Commands;

public record UpdateUserCommand(Guid Id) : IRequest<UserResponse>;