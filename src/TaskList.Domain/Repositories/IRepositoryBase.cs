namespace TaskList.Domain.Repositories;

public interface IRepositoryBase<T>
{
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<T> GetByIdAsync(Guid entityId, CancellationToken cancellationToken = default);
    Task<T> Insert(T entity);
    Task Remove(T entity);
}