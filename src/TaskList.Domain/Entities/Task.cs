namespace TaskList.Domain.Entities;

public class Task
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public TaskList TaskList { get; set; }
}