using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using TaskList.Contracts.Commands;
using TaskList.Contracts.Responses;
using TaskList.Domain.Entities;
using TaskList.Domain.Repositories;

namespace TaskList.Services.Handlers;

public class CommandHandlersTaskStatusRecord : IRequestHandler<CommandTaskStatusRecordCreate, ResponseTaskStatusRecord>
{
    private readonly IRepository<Domain.Entities.TaskStatusRecord> _repositoryRecord;
    private readonly IRepositoryReadOnly<Domain.Entities.Task> _repositoryTask;
    private readonly IRepositoryReadOnly<Domain.Entities.TaskStatus> _repositoryStatus;
    private readonly IMapper _mapper;

    public CommandHandlersTaskStatusRecord(IRepository<TaskStatusRecord> repositoryRecord,
        IRepositoryReadOnly<Domain.Entities.Task> repositoryTask,
        IRepositoryReadOnly<Domain.Entities.TaskStatus> repositoryStatus, IMapper mapper)
    {
        _repositoryRecord = repositoryRecord;
        _repositoryTask = repositoryTask;
        _repositoryStatus = repositoryStatus;
        _mapper = mapper;
    }

    public async Task<ResponseTaskStatusRecord> Handle(CommandTaskStatusRecordCreate request, CancellationToken cancellationToken)
    {
        var status = await _repositoryStatus.GetByIdAsync(request.TaskStatusId, cancellationToken);
        Guard.Against.NotFound(request.TaskStatusId, status, nameof(status.Id));
        var task = await _repositoryTask.GetByIdAsync(request.TaskId, cancellationToken);
        Guard.Against.NotFound(request.TaskId, task, nameof(task.Id));
        var newRecord = _mapper.Map<Domain.Entities.TaskStatusRecord>(request);
        newRecord.DateTime = DateTime.UtcNow;
        var record = await _repositoryRecord.AddAsync(newRecord, cancellationToken);
        var response = _mapper.Map<ResponseTaskStatusRecord>(record);
        return response with { StatusName = status.Name };
    }
}