using Ardalis.GuardClauses;
using AutoMapper;
using MediatR;
using TaskList.Contracts.Commands;
using TaskList.Contracts.Responses;
using TaskList.Domain.Repositories;

namespace TaskList.Services.Handlers;

public class CommandHandlersTaskComment :
    IRequestHandler<CommandTaskCommentCreate, ResponseTaskComment>,
    IRequestHandler<CommandTaskCommentUpdate, ResponseTaskComment>,
    IRequestHandler<CommandTaskCommentDelete>
{
    private readonly IRepository<Domain.Entities.TaskComment> _repositoryComment;
    private readonly IRepositoryReadOnly<Domain.Entities.Task> _repositoryTask;
    private readonly IMapper _mapper;

    public CommandHandlersTaskComment(IRepository<Domain.Entities.TaskComment> repositoryComment,
        IRepositoryReadOnly<Domain.Entities.Task> repositoryTask, IMapper mapper)
    {
        _repositoryComment = repositoryComment;
        _repositoryTask = repositoryTask;
        _mapper = mapper;
    }

    public async Task<ResponseTaskComment> Handle(CommandTaskCommentCreate request, CancellationToken cancellationToken)
    {
        var task = await _repositoryTask.GetByIdAsync(request.TaskId, cancellationToken);
        Guard.Against.NotFound(request.TaskId, task, nameof(task.Id));
        var newComment = _mapper.Map<Domain.Entities.TaskComment>(request);
        var result = await _repositoryComment.AddAsync(newComment, cancellationToken);
        return _mapper.Map<ResponseTaskComment>(result);
    }

    public async Task<ResponseTaskComment> Handle(CommandTaskCommentUpdate request, CancellationToken cancellationToken)
    {
        var exist = await _repositoryComment.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, exist, nameof(exist.Id));
        var update = _mapper.Map<Domain.Entities.TaskComment>(request);
        await _repositoryComment.UpdateAsync(update, cancellationToken);
        var result = await _repositoryComment.GetByIdAsync(update.Id, cancellationToken);
        return _mapper.Map<ResponseTaskComment>(result);
    }

    public async Task Handle(CommandTaskCommentDelete request, CancellationToken cancellationToken)
    {
        var exist = await _repositoryComment.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, exist, nameof(exist.Id));
        await _repositoryComment.DeleteAsync(exist, cancellationToken);
    }
}