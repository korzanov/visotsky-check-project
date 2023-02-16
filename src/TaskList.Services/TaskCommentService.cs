using TaskList.Contracts;
using TaskList.Domain.Repositories;
using TaskList.Services.Abstractions;

namespace TaskList.Services;

internal sealed class TaskCommentService : ServiceBase, ITaskCommentService
{
    public TaskCommentService(IRepositoryManager repositoryManager) : base(repositoryManager)
    { }

    public async Task<IEnumerable<TaskCommentDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<TaskCommentDto> GetByIdAsync(Guid entityId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<TaskCommentDto> CreateAsync(TaskCommentCreateDto createDto, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(Guid entityId, TaskCommentUpdateDto updateDto, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Guid entityId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}