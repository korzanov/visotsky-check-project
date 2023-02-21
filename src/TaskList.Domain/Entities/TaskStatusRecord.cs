namespace TaskList.Domain.Entities;

public class TaskStatusRecord
{
    public Guid Id { get; set; }
    public Guid TaskStatusId { get; set; }
    public Guid TaskId { get; set; }
    public DateTime DateTime { get; set; }
}