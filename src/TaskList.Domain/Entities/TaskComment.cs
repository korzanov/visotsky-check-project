namespace TaskList.Domain.Entities;

public class TaskComment
{
    public Guid Id { get; set; }
    public Guid TaskId { get; set; }
    public string? Message { get; set; }
}