using Ardalis.Specification;

namespace TaskList.Domain.Repositories;

public interface IRepositoryReadOnly<T> : IReadRepositoryBase<T> where T : class { }