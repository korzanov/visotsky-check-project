namespace TaskList.Domain.Entities;

public class TaskComment
{
    public Guid Id { get; set; }
    public Task Task { get; set; }
    public string Message { get; set; }
}