using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using TaskList.Contracts.Commands;
using TaskList.Contracts.Responses;
using TaskList.Domain.Repositories;

namespace TaskList.Services.Handlers;

public class CommandHandlersTask :
    IRequestHandler<CommandTaskCreate, ResponseTask>
{
    private readonly IRepository<TaskList.Domain.Entities.Task> _taskRepository;
    private readonly IRepositoryReadOnly<TaskList.Domain.Entities.TaskList> _taskListRepository;
    private readonly IMapper _mapper;

    public CommandHandlersTask(
        IRepository<TaskList.Domain.Entities.Task> taskRepository, 
        IRepositoryReadOnly<Domain.Entities.TaskList> taskListRepository, 
        IMapper mapper)
    {
        _taskRepository = taskRepository;
        _taskListRepository = taskListRepository;
        _mapper = mapper;
    }

    public async Task<ResponseTask> Handle(CommandTaskCreate request, CancellationToken cancellationToken)
    {
        var taskList = await _taskListRepository.GetByIdAsync(request.TaskListId, cancellationToken);
        Guard.Against.NotFound(request.TaskListId, taskList, nameof(taskList.Id));
        var taskToCreate = _mapper.Map<Domain.Entities.Task>(request);
        var createdTask = await _taskRepository.AddAsync(taskToCreate, cancellationToken);
        return _mapper.Map<ResponseTask>(createdTask);
    }
}