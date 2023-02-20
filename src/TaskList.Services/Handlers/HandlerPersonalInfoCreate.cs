using MediatR;
using TaskList.Contracts.Commands;
using TaskList.Contracts.Responses;
using TaskList.Domain.Repositories;

namespace TaskList.Services.Handlers;

public class HandlerPersonalInfoCreate : IRequestHandler<CommandPersonalInfoCreate, ResponsePersonalInfo>
{
    private readonly IRepositoryPersonalInfo _repository;
    
    public HandlerPersonalInfoCreate(IRepositoryPersonalInfo repository)
    {
        _repository = repository;
    }

    public async Task<ResponsePersonalInfo> Handle(CommandPersonalInfoCreate request, CancellationToken cancellationToken)
    {
        await _repository.CreatePersonalInfo(request.Login, request.Password);
        var user = await _repository.GetPersonalInfo(request.Login);
        return new ResponsePersonalInfo(user.Login, user.Name, user.Email);
    }
}