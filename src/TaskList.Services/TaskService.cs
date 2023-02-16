using TaskList.Contracts;
using TaskList.Domain.Repositories;
using TaskList.Services.Abstractions;

namespace TaskList.Services;

internal sealed class TaskService : ServiceBase, ITaskService
{
    public TaskService(IRepositoryManager repositoryManager) : base(repositoryManager)
    { }

    public async Task<IEnumerable<TaskDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<TaskDto> GetByIdAsync(Guid entityId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<TaskDto> CreateAsync(TaskCreateDto createDto, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(Guid entityId, TaskUpdateDto updateDto, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid entityId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}