using Ardalis.GuardClauses;
using Ardalis.Specification;
using AutoMapper;
using MediatR;
using TaskList.Contracts.Queries;
using TaskList.Contracts.Responses;
using TaskList.Domain.Repositories;
using TaskList.Domain.Specifications;

namespace TaskList.Services.Handlers;

public class QueryHandlersTaskList : 
    IRequestHandler<QueryTaskListGet, ResponseTaskList>,
    IRequestHandler<QueryTaskListGetAll, IEnumerable<ResponseTaskList>>,
    IRequestHandler<QueryTaskListGetPage, IEnumerable<ResponseTaskList>>
{
    private readonly IRepositoryReadOnly<Domain.Entities.TaskList> _repository;
    private readonly IMapper _mapper;
    private readonly int _defaultPageSize;

    public QueryHandlersTaskList(IRepositoryReadOnly<Domain.Entities.TaskList> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
        _defaultPageSize = 5;
    }

    public async Task<ResponseTaskList> Handle(QueryTaskListGet request, CancellationToken cancellationToken)
    {
        var taskList = await _repository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, taskList, nameof(taskList.Id));
        return _mapper.Map<ResponseTaskList>(taskList);
    }

    public async Task<IEnumerable<ResponseTaskList>> Handle(QueryTaskListGetAll request, CancellationToken cancellationToken)
    {
        var taskLists = await _repository.ListAsync(cancellationToken);
        return taskLists.Select(taskList => _mapper.Map<ResponseTaskList>(taskList));
    }

    public async Task<IEnumerable<ResponseTaskList>> Handle(QueryTaskListGetPage request, CancellationToken cancellationToken)
    {
        var pageConfig = GetPageSpec(request.PageNumber);
        var taskListPage = await _repository.ListAsync(pageConfig, cancellationToken);
        return taskListPage.Select(taskList => _mapper.Map<ResponseTaskList>(taskList));
    }

    private ISpecification<Domain.Entities.TaskList> GetPageSpec(int pageNumber)
    {
        var skipSize = pageNumber * _defaultPageSize - _defaultPageSize;
        return new SpecCommon<Domain.Entities.TaskList>(skipSize, pageNumber);
    }
}