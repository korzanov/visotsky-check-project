using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using TaskList.Contracts.Queries;
using TaskList.Contracts.Responses;
using TaskList.Domain.Entities;
using TaskList.Domain.Repositories;
using TaskList.Domain.Specifications;

namespace TaskList.Services.Handlers;

public class QUeryHandlersTaskComment : IRequestHandler<QueryTaskCommentGetAll, IEnumerable<ResponseTaskComment>>
{
    private readonly IRepositoryReadOnly<Domain.Entities.TaskComment> _repositoryComment;
    private readonly IRepositoryReadOnly<Domain.Entities.Task> _repositoryTask;
    private readonly IMapper _mapper;

    public QUeryHandlersTaskComment(IRepositoryReadOnly<TaskComment> repositoryComment, IMapper mapper, IRepositoryReadOnly<Domain.Entities.Task> repositoryTask)
    {
        _repositoryComment = repositoryComment;
        _mapper = mapper;
        _repositoryTask = repositoryTask;
    }

    public async Task<IEnumerable<ResponseTaskComment>> Handle(QueryTaskCommentGetAll request, CancellationToken cancellationToken)
    {
        var existTask = await _repositoryTask.GetByIdAsync(request.TaskId, cancellationToken);
        Guard.Against.NotFound(request.TaskId, existTask, nameof(existTask.Id));
        var spec = new SpecCommon<TaskComment>(filter: comment => comment.TaskId == request.TaskId);
        var result = await _repositoryComment.ListAsync(spec, cancellationToken);
        return result.Select(comment => _mapper.Map<ResponseTaskComment>(comment));
    }
}