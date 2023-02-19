using MediatR;
using TaskList.Contracts.Commands;
using TaskList.Contracts.Responses;

namespace TaskList.Services.Handlers;

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand,UserResponse>
{
    public Task<UserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}