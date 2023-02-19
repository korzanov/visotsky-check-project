namespace TaskList.Domain.Entities;

public interface IPersonalInfo
{
    string UserName { get; }
    string? Name { get; set; }
    string? Email { get; set; }
}