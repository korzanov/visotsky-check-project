using MediatR;
using TaskList.Contracts.Commands;
using TaskList.Contracts.Responses;
using TaskList.Domain.Entities;
using TaskList.Domain.Interfaces;

namespace TaskList.Services.Handlers;

internal record PersonalInfoMiddleObject(string Login, string Name, string Email) : IPersonalInfo;
public class UpdateUserHandler : IRequestHandler<CommandPersonalInfoUpdate,ResponsePersonalInfo>
{
    private readonly IPersonalInfoRepository _repository;

    public UpdateUserHandler(IPersonalInfoRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResponsePersonalInfo> Handle(CommandPersonalInfoUpdate request, CancellationToken cancellationToken)
    {
        var personalInfo = new PersonalInfoMiddleObject(request.Login, request.Name, request.Email);
        var result = await _repository.UpdatePersonalInfo(personalInfo);
        return new ResponsePersonalInfo(result.Login, result.Name, result.Email);
    }
}