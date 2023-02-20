using MediatR;
using TaskList.Contracts.Commands;
using TaskList.Contracts.Responses;
using TaskList.Domain.Entities;
using TaskList.Domain.Interfaces;

namespace TaskList.Services.Handlers;

public class UpdateUserHandler : IRequestHandler<UpdatePersonalInfoCommand,PersonalInfoResponse>
{
    private readonly IPersonalInfoRepository _repository;

    public UpdateUserHandler(IPersonalInfoRepository repository)
    {
        _repository = repository;
    }

    public async Task<PersonalInfoResponse> Handle(UpdatePersonalInfoCommand request, CancellationToken cancellationToken)
    {
        var personalInfo = new PersonalInfo(request.Login) { Name = request.Name, Email = request.Email};
        var result = await _repository.UpdatePersonalInfo(personalInfo);
        return new PersonalInfoResponse(result.UserName, result.Name, result.Email);
    }
}