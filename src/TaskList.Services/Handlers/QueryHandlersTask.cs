using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using TaskList.Contracts.Queries;
using TaskList.Contracts.Responses;
using TaskList.Domain.Repositories;
using TaskList.Domain.Specifications;

namespace TaskList.Services.Handlers;

public class QueryHandlersTask :
    IRequestHandler<QueryTaskGet, ResponseTask>,
    IRequestHandler<QueryTaskGetAll, IEnumerable<ResponseTask>>,
    IRequestHandler<QueryTaskGetAllByTaskList, IEnumerable<ResponseTask>>
{
    private readonly IRepositoryReadOnly<Domain.Entities.Task> _repository;
    private readonly IRepositoryReadOnly<Domain.Entities.TaskList> _repositoryTaskList;
    private readonly IMapper _mapper;

    public QueryHandlersTask(
        IRepositoryReadOnly<Domain.Entities.Task> repository, 
        IRepositoryReadOnly<Domain.Entities.TaskList> repositoryTaskList, 
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
        _repositoryTaskList = repositoryTaskList;
    }

    public async Task<ResponseTask> Handle(QueryTaskGet request, CancellationToken cancellationToken)
    {
        var existTask = await _repository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, existTask, nameof(existTask.Id));
        return _mapper.Map<ResponseTask>(existTask);
    }

    public async Task<IEnumerable<ResponseTask>> Handle(QueryTaskGetAll request, CancellationToken cancellationToken)
    {
        var allTasks = await _repository.ListAsync(cancellationToken);
        return allTasks.Select(task => _mapper.Map<ResponseTask>(task));
    }

    public async Task<IEnumerable<ResponseTask>> Handle(QueryTaskGetAllByTaskList request, CancellationToken cancellationToken)
    {
        var existTaskList = await _repositoryTaskList.GetByIdAsync(request.TaskListId, cancellationToken);
        Guard.Against.NotFound(request.TaskListId, existTaskList, nameof(existTaskList.Id));
        var spec = new SpecCommon<Domain.Entities.Task>(task => task.TaskListId == request.TaskListId);
        var taskListTasks = await _repository.ListAsync(spec, cancellationToken);
        return taskListTasks.Select(task => _mapper.Map<ResponseTask>(task));
    }
}