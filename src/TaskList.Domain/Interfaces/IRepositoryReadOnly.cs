using Ardalis.Specification;

namespace TaskList.Domain.Interfaces;

public interface IRepositoryReadOnly<T> : IReadRepositoryBase<T> where T : class { }