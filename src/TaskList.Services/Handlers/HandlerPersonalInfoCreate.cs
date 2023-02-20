using AutoMapper;
using MediatR;
using TaskList.Contracts.Commands;
using TaskList.Contracts.Responses;
using TaskList.Domain.Repositories;

namespace TaskList.Services.Handlers;

public class HandlerPersonalInfoCreate : IRequestHandler<CommandPersonalInfoCreate, ResponsePersonalInfo>
{
    private readonly IRepositoryPersonalInfo _repository;
    private readonly IMapper _mapper;
    
    public HandlerPersonalInfoCreate(IRepositoryPersonalInfo repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponsePersonalInfo> Handle(CommandPersonalInfoCreate request, CancellationToken cancellationToken)
    {
        await _repository.CreatePersonalInfo(request.Login, request.Password);
        var personalInfo = await _repository.GetPersonalInfo(request.Login);
        return _mapper.Map<ResponsePersonalInfo>(personalInfo);
    }
}