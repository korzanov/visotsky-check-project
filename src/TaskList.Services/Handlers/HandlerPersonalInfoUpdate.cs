using AutoMapper;
using MediatR;
using TaskList.Contracts.Commands;
using TaskList.Contracts.Responses;
using TaskList.Domain.Entities;
using TaskList.Domain.Repositories;

namespace TaskList.Services.Handlers;


public class HandlerPersonalInfoUpdate : IRequestHandler<CommandPersonalInfoUpdate,ResponsePersonalInfo>
{
    private readonly IRepositoryPersonalInfo _repository;
    private readonly IMapper _mapper;

    public HandlerPersonalInfoUpdate(IRepositoryPersonalInfo repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponsePersonalInfo> Handle(CommandPersonalInfoUpdate request, CancellationToken cancellationToken)
    {
        var newPersonalInfo = _mapper.Map<PersonalInfoMiddleObject>(request);
        var personalInfo = await _repository.UpdatePersonalInfo(newPersonalInfo);
        return _mapper.Map<ResponsePersonalInfo>(personalInfo);
    }
    
}