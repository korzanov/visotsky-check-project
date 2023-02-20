namespace TaskList.Domain.Entities;

public interface IPersonalInfo
{
    public string Login { get; }
    public string? Name { get; }
    public string? Email { get; }
}