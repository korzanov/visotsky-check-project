using TaskList.Contracts;
using TaskList.Domain.Repositories;
using TaskList.Services.Abstractions;

namespace TaskList.Services;

internal sealed class TaskListService : ServiceBase, ITaskListService
{
    public TaskListService(IRepositoryManager repositoryManager) : base(repositoryManager)
    { }

    public async Task<IEnumerable<TaskListDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<TaskListDto> GetByIdAsync(Guid entityId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<TaskListDto> CreateAsync(TaskListCreateDto createDto, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(Guid entityId, TaskListUpdateDto updateDto, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Guid entityId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}