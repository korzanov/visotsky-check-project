using MediatR;
using TaskList.Contracts.Commands;
using TaskList.Contracts.Responses;
using TaskList.Domain.Entities;
using TaskList.Domain.Repositories;

namespace TaskList.Services.Handlers;


public class HandlerPersonalInfoUpdate : IRequestHandler<CommandPersonalInfoUpdate,ResponsePersonalInfo>
{
    private readonly IRepositoryPersonalInfo _repository;

    public HandlerPersonalInfoUpdate(IRepositoryPersonalInfo repository)
    {
        _repository = repository;
    }

    public async Task<ResponsePersonalInfo> Handle(CommandPersonalInfoUpdate request, CancellationToken cancellationToken)
    {
        var personalInfo = new PersonalInfoMiddleObject(request.Login, request.Name, request.Email);
        var result = await _repository.UpdatePersonalInfo(personalInfo);
        return new ResponsePersonalInfo(result.Login, result.Name, result.Email);
    }
    
    private record PersonalInfoMiddleObject(string Login, string Name, string Email) : IPersonalInfo;
}