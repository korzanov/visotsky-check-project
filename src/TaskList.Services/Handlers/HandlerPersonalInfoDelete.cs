using MediatR;
using TaskList.Contracts.Commands;
using TaskList.Domain.Repositories;

namespace TaskList.Services.Handlers;

public class HandlerPersonalInfoDelete : IRequestHandler<CommandPersonalInfoDelete>
{
    private readonly IRepositoryPersonalInfo _repository;

    public HandlerPersonalInfoDelete(IRepositoryPersonalInfo repository)
    {
        _repository = repository;
    }

    public async Task Handle(CommandPersonalInfoDelete request, CancellationToken cancellationToken)
    {
        await _repository.DeletePersonalInfo(request.Login);
    }
}