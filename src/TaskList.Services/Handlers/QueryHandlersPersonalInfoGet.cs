using AutoMapper;
using MediatR;
using TaskList.Contracts.Queries;
using TaskList.Contracts.Responses;
using TaskList.Domain.Repositories;

namespace TaskList.Services.Handlers;

public class QueryHandlersPersonalInfoGet : 
    IRequestHandler<QueryPersonalInfoGet, ResponsePersonalInfo>
{
    private readonly IRepositoryPersonalInfo _repository;
    private readonly IMapper _mapper;

    public QueryHandlersPersonalInfoGet(IRepositoryPersonalInfo repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponsePersonalInfo> Handle(QueryPersonalInfoGet request, CancellationToken cancellationToken)
    {
        var personalInfo = await _repository.GetPersonalInfo(request.Login);
        return _mapper.Map<ResponsePersonalInfo>(personalInfo);
    }
}