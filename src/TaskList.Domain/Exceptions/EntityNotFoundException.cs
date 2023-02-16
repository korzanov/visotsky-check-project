namespace TaskList.Domain.Exceptions;

public class EntityNotFoundException<T> : NotFoundException
{
    public EntityNotFoundException(Guid entityId, string entityName)
        : base($"The {entityName} with the identifier {entityId} was not found.")
    { }
}