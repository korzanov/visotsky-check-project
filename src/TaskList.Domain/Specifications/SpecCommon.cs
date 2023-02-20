using System.Linq.Expressions;
using Ardalis.Specification;

namespace TaskList.Domain.Specifications;

public sealed class SpecCommon<T> : Specification<T>
{
    public SpecCommon(Expression<Func<T, bool>> filter)
    {
        Query.Where(filter);
    }
    
    public SpecCommon(int skip, int take)
    {
        take = take > 0 ? take : int.MaxValue;
        Query.Skip(skip).Take(take);
    }
    
    public SpecCommon(int skip, int take, Expression<Func<T, bool>> filter)
    {
        take = take > 0 ? take : int.MaxValue;
        Query.Where(filter).Skip(skip).Take(take);
    }

    public SpecCommon(Expression<Func<T, object?>> order)
    {
        Query.OrderBy(order);
    }

    public SpecCommon(int skip, int take, Expression<Func<T, object?>> order)
    {
        Query.OrderBy(order).Skip(skip).Take(take);
    }

    public SpecCommon(int skip, int take, Expression<Func<T, bool>> filter, Expression<Func<T, object?>> order)
    {
        Query.Where(filter).OrderBy(order).Skip(skip).Take(take);
    }
}