namespace TaskList.Domain.Entities;

public class TaskStatusRecord
{
    public Guid Id { get; set; }
    
    public TaskStatus Status { get; set; }
    
    public Task Task { get; set; }
    
    public DateTime DateTime { get; set; }
}