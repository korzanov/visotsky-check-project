using MediatR;
using TaskList.Contracts.Queries;
using TaskList.Contracts.Responses;
using TaskList.Domain.Repositories;

namespace TaskList.Services.Handlers;

public class HandlerPersonalInfoGet : IRequestHandler<QueryPersonalInfoGet, ResponsePersonalInfo>
{
    private readonly IRepositoryPersonalInfo _repository;

    public HandlerPersonalInfoGet(IRepositoryPersonalInfo repository)
    {
        _repository = repository;
    }

    public async Task<ResponsePersonalInfo> Handle(QueryPersonalInfoGet request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetPersonalInfo(request.Login);
        return new ResponsePersonalInfo(result.Login, result.Name, result.Email);
    }
}