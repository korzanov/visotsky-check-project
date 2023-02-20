using System.Linq.Expressions;
using Ardalis.Specification;

namespace TaskList.Domain.Specifications;

public sealed class CommonSpec<T> : Specification<T>
{
    public CommonSpec(Expression<Func<T, bool>> where)
    {
        Query.Where(where);
    }
    
    public CommonSpec(int skip, int take)
    {
        take = take > 0 ? take : int.MaxValue;
        Query.Skip(skip).Take(take);
    }
    
    public CommonSpec(int skip, int take, Expression<Func<T, bool>> where)
    {
        take = take > 0 ? take : int.MaxValue;
        Query.Where(where).Skip(skip).Take(take);
    }

    public CommonSpec(Expression<Func<T, object?>> order)
    {
        Query.OrderBy(order);
    }

    public CommonSpec(int skip, int take, Expression<Func<T, object?>> order)
    {
        Query.OrderBy(order).Skip(skip).Take(take);
    }

    public CommonSpec(int skip, int take, Expression<Func<T, bool>> where, Expression<Func<T, object?>> order)
    {
        Query.Where(where).OrderBy(order).Skip(skip).Take(take);
    }
}