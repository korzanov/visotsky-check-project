using Ardalis.Specification.EntityFrameworkCore;
using TaskList.DbInfrastructure.Data;
using TaskList.Domain.Repositories;

namespace TaskList.DbInfrastructure.Data;

public class EfRepository<T> : RepositoryBase<T>, IRepository<T>, IRepositoryReadOnly<T> where T : class
{
    // ReSharper disable once SuggestBaseTypeForParameterInConstructor
    public EfRepository(RepositoryDbContext dbContext) : base(dbContext) { }
}