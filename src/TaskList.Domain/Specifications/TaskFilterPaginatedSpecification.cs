using Ardalis.Specification;
using Task = TaskList.Domain.Entities.Task;

namespace TaskList.Domain.Specifications;

public sealed class TaskFilterPaginatedSpecification : Specification<Task>
{
    public TaskFilterPaginatedSpecification(int skip, int take, Guid? taskListId)
    {
        take = take > 0 ? take : int.MaxValue;
        Query
            .Where(i => taskListId.HasValue && i.TaskList.Id == taskListId)
            .Skip(skip).Take(take);
    }
}