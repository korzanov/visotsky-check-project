using Ardalis.Specification;

namespace TaskList.Domain.Interfaces;

public interface IRepository<T> : IRepositoryBase<T> where T : class { }