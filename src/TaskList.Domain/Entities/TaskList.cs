namespace TaskList.Domain.Entities;

public class TaskList
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}