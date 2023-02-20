using MediatR;
using TaskList.Contracts.Commands;
using TaskList.Domain.Interfaces;

namespace TaskList.Services.Handlers;

public class HandlerPersonalInfoDelete : IRequestHandler<CommandPersonalInfoDelete>
{
    private readonly IPersonalInfoRepository _repository;

    public HandlerPersonalInfoDelete(IPersonalInfoRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(CommandPersonalInfoDelete request, CancellationToken cancellationToken)
    {
        await _repository.DeletePersonalInfo(request.Login);
    }
}