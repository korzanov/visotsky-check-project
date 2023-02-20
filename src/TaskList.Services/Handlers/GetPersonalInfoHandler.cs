using MediatR;
using TaskList.Contracts.Queries;
using TaskList.Contracts.Responses;
using TaskList.Domain.Interfaces;

namespace TaskList.Services.Handlers;

public class GetPersonalInfoHandler : IRequestHandler<GetPersonalInfoQuery, PersonalInfoResponse>
{
    private readonly IPersonalInfoRepository _repository;

    public GetPersonalInfoHandler(IPersonalInfoRepository repository)
    {
        _repository = repository;
    }

    public async Task<PersonalInfoResponse> Handle(GetPersonalInfoQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetPersonalInfo(request.Login);
        return new PersonalInfoResponse(result.UserName, result.Name, result.Email);
    }
}