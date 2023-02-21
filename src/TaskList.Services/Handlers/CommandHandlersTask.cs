using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using TaskList.Contracts.Commands;
using TaskList.Contracts.Responses;
using TaskList.Domain.Repositories;

namespace TaskList.Services.Handlers;

public class CommandHandlersTask :
    IRequestHandler<CommandTaskCreate, ResponseTask>,
    IRequestHandler<CommandTaskUpdate, ResponseTask>,
    IRequestHandler<CommandTaskChangeTaskList, ResponseTask>,
    IRequestHandler<CommandTaskDelete>
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
        taskToCreate.CreatedAt = DateTime.UtcNow;
        var createdTask = await _taskRepository.AddAsync(taskToCreate, cancellationToken);
        return _mapper.Map<ResponseTask>(createdTask);
    }

    public async Task<ResponseTask> Handle(CommandTaskUpdate request, CancellationToken cancellationToken)
    {
        var existTask = await _taskRepository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, existTask, nameof(existTask.Id));
        var taskToUpdate = _mapper.Map<Domain.Entities.Task>(request);
        taskToUpdate.TaskListId = existTask.TaskListId;
        await _taskRepository.UpdateAsync(taskToUpdate, cancellationToken);
        var updatedTask = await _taskRepository.GetByIdAsync(taskToUpdate.Id, cancellationToken);
        return _mapper.Map<ResponseTask>(updatedTask);
    }

    public async Task Handle(CommandTaskDelete request, CancellationToken cancellationToken)
    {
        var existTask = await _taskRepository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, existTask, nameof(existTask.Id));
        await _taskRepository.DeleteAsync(existTask, cancellationToken);
    }

    public async Task<ResponseTask> Handle(CommandTaskChangeTaskList request, CancellationToken cancellationToken)
    {
        var taskList = await _taskListRepository.GetByIdAsync(request.TaskListId, cancellationToken);
        Guard.Against.NotFound(request.TaskListId, taskList, nameof(taskList.Id));
        var existTask = await _taskRepository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, existTask, nameof(existTask.Id));
        existTask.TaskListId = request.TaskListId;
        var updatedTask = await _taskRepository.GetByIdAsync(existTask.Id, cancellationToken);
        return _mapper.Map<ResponseTask>(updatedTask);
    }
}