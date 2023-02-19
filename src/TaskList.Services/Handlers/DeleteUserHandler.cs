using MediatR;
using TaskList.Contracts.Commands;

namespace TaskList.Services.Handlers;

public class DeleteUserHandler : IRequestHandler<DeleteUserCommand>
{
    public Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}