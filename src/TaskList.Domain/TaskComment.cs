namespace TaskList.Domain;

public class TaskComment
{
    public Guid Id { get; set; }
    public User User { get; set; }
    public string Message { get; set; }
}