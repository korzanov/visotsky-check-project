using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using TaskList.Contracts.Queries;
using TaskList.Contracts.Responses;
using TaskList.Domain.Repositories;
using Task = TaskList.Domain.Entities.Task;

namespace TaskList.Services.Handlers;

public class QueryHandlersTask :
    IRequestHandler<QueryTaskGet, ResponseTask>,
    IRequestHandler<QueryTaskGetAll, IEnumerable<ResponseTask>>
{
    private readonly IRepositoryReadOnly<Domain.Entities.Task> _repository;
    private readonly IMapper _mapper;

    public QueryHandlersTask(IRepositoryReadOnly<Task> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
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
}