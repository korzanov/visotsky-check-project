using Ardalis.Specification.EntityFrameworkCore;
using TaskList.DbInfrastructure.Data;

namespace TaskList.DbInfrastructure.Data;

public class EfRepository<T> : RepositoryBase<T> where T : class
{
    // ReSharper disable once SuggestBaseTypeForParameterInConstructor
    public EfRepository(RepositoryDbContext dbContext) : base(dbContext) { }
}