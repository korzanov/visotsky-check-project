using MediatR;
using TaskList.Contracts.Commands;
using TaskList.Contracts.Responses;
using TaskList.Domain.Entities;
using TaskList.Domain.Interfaces;

namespace TaskList.Services.Handlers;

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand,UserResponse>
{
    private readonly IPersonalInfoRepository _repository;

    public UpdateUserHandler(IPersonalInfoRepository repository)
    {
        _repository = repository;
    }

    public async Task<UserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var personalInfo = new PersonalInfo(request.Login);
        var result = await _repository.UpdatePersonalInfo(personalInfo);
        return new UserResponse(result.UserName, result.Name, result.Email);
    }
}