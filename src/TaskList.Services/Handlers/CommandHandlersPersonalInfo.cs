using AutoMapper;
using MediatR;
using TaskList.Contracts.Commands;
using TaskList.Contracts.Responses;
using TaskList.Domain.Repositories;

namespace TaskList.Services.Handlers;

public class CommandHandlersPersonalInfo : 
    IRequestHandler<CommandPersonalInfoCreate, ResponsePersonalInfo>,
    IRequestHandler<CommandPersonalInfoUpdate,ResponsePersonalInfo>,
    IRequestHandler<CommandPersonalInfoDelete>
{
    private readonly IRepositoryPersonalInfo _repository;
    private readonly IMapper _mapper;
    
    public CommandHandlersPersonalInfo(IRepositoryPersonalInfo repository, IMapper mapper)
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

    public async Task<ResponsePersonalInfo> Handle(CommandPersonalInfoUpdate request, CancellationToken cancellationToken)
    {
        var newPersonalInfo = _mapper.Map<PersonalInfoMiddleObject>(request);
        var personalInfo = await _repository.UpdatePersonalInfo(newPersonalInfo);
        return _mapper.Map<ResponsePersonalInfo>(personalInfo);
    }
    
    public async Task Handle(CommandPersonalInfoDelete request, CancellationToken cancellationToken)
    {
        await _repository.DeletePersonalInfo(request.Login);
    }
}