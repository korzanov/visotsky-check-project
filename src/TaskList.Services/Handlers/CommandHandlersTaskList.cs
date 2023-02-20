using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using TaskList.Contracts.Commands;
using TaskList.Contracts.Responses;
using TaskList.Domain.Repositories;

namespace TaskList.Services.Handlers;

public class CommandHandlersTaskList : 
    IRequestHandler<CommandTaskListCreate, ResponseTaskList>,
    IRequestHandler<CommandTaskListUpdate, ResponseTaskList>,
    IRequestHandler<CommandTaskListDelete>
{
    private readonly IRepository<Domain.Entities.TaskList> _repository;
    private readonly IMapper _mapper;

    public CommandHandlersTaskList(IRepository<Domain.Entities.TaskList> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseTaskList> Handle(CommandTaskListCreate request, CancellationToken cancellationToken)
    {
        var newTaskList = _mapper.Map<Domain.Entities.TaskList>(request);
        var taskList = await _repository.AddAsync(newTaskList, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);
        var taskListResult = await _repository.GetByIdAsync(taskList.Id, cancellationToken);
        return _mapper.Map<ResponseTaskList>(taskListResult);
    }

    public async Task<ResponseTaskList> Handle(CommandTaskListUpdate request, CancellationToken cancellationToken)
    {
        var changeTaskList = _mapper.Map<Domain.Entities.TaskList>(request);
        await _repository.UpdateAsync(changeTaskList, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);
        var taskListResult = await _repository.GetByIdAsync(changeTaskList.Id, cancellationToken);
        return _mapper.Map<ResponseTaskList>(taskListResult);
    }

    public async Task Handle(CommandTaskListDelete request, CancellationToken cancellationToken)
    {
        var taskList = await _repository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, taskList, nameof(taskList.Id));
        await _repository.DeleteAsync(taskList, cancellationToken);
    }
}