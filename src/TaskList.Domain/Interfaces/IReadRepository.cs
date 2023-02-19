using Ardalis.Specification;

namespace TaskList.Domain.Interfaces;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class { }