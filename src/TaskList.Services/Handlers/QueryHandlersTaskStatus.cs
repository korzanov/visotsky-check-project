using AutoMapper;
using MediatR;
using TaskList.Contracts.Queries;
using TaskList.Contracts.Responses;
using TaskList.Domain.Repositories;

namespace TaskList.Services.Handlers;

public class QueryHandlersTaskStatus :
    IRequestHandler<QueryTaskStatusGetDefault, ResponseTaskStatus>,
    IRequestHandler<QueryTaskStatusGetAll, IEnumerable<ResponseTaskStatus>>
{
    private readonly IRepositoryReadOnly<Domain.Entities.TaskStatus> _repository;
    private readonly IMapper _mapper;

    public QueryHandlersTaskStatus(IRepositoryReadOnly<Domain.Entities.TaskStatus> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseTaskStatus> Handle(QueryTaskStatusGetDefault request, CancellationToken cancellationToken)
    {
        var defaultTaskStatus = await _repository.GetByIdAsync(ResponseTaskStatus.Default.Id, cancellationToken);
        return _mapper.Map<ResponseTaskStatus>(defaultTaskStatus);
    }

    public async Task<IEnumerable<ResponseTaskStatus>> Handle(QueryTaskStatusGetAll request, CancellationToken cancellationToken)
    {
        var taskStatuses = await _repository.ListAsync(cancellationToken);
        return taskStatuses.Select(taskStatus => _mapper.Map<ResponseTaskStatus>(taskStatus));
    }
}