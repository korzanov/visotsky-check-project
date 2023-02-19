using MediatR;
using TaskList.Contracts.Queries;
using TaskList.Contracts.Responses;

namespace TaskList.Services.Handlers;

public class AuthHandler : IRequestHandler<AuthQuery, AuthResponse>
{
    public Task<AuthResponse> Handle(AuthQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}