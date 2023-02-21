using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using TaskList.Contracts.Queries;
using TaskList.Contracts.Responses;
using TaskList.Domain.Repositories;
using TaskList.Domain.Specifications;

namespace TaskList.Services.Handlers;

public class QueryHandlersTaskStatusRecord : IRequestHandler<QueryTaskStatusRecordGetLast, ResponseTaskStatusRecord>
{
    private readonly IRepositoryReadOnly<Domain.Entities.TaskStatusRecord> _repositoryRecord;
    private readonly IRepositoryReadOnly<Domain.Entities.TaskStatus> _repositoryStatus;
    private readonly IMapper _mapper;

    public QueryHandlersTaskStatusRecord(
        IRepositoryReadOnly<Domain.Entities.TaskStatusRecord> repositoryRecord, 
        IRepositoryReadOnly<Domain.Entities.TaskStatus> repositoryStatus, 
        IMapper mapper)
    {
        _repositoryRecord = repositoryRecord;
        _repositoryStatus = repositoryStatus;
        _mapper = mapper;
    }

    public async Task<ResponseTaskStatusRecord> Handle(QueryTaskStatusRecordGetLast request, CancellationToken cancellationToken)
    {
        var spec = new SpecSingleCommon<Domain.Entities.TaskStatusRecord>(
            filter: record => record.TaskId == request.TaskId,
            order: record => record.DateTime,
            orderByDescending: true);
        var record = await _repositoryRecord.SingleOrDefaultAsync(spec, cancellationToken);
        Guard.Against.NotFound(request.TaskId, record, nameof(record.TaskId));
        var status = await _repositoryStatus.GetByIdAsync(record.TaskStatusId, cancellationToken);
        Guard.Against.NotFound(record.TaskStatusId, status, nameof(status.Id));
        var response = _mapper.Map<ResponseTaskStatusRecord>(record);
        return response with { StatusName = status.Name };
    }
}