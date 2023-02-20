using Ardalis.Specification;

namespace TaskList.Domain.Repositories;

public interface IRepository<T> : IRepositoryBase<T> where T : class { }