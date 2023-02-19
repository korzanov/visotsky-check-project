using MediatR;
using TaskList.Contracts.Responses;

namespace TaskList.Contracts.Queries;

public record AuthQuery(string Login, string Password) : IRequest<AuthResponse>;