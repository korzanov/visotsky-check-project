namespace TaskList.Domain.Entities;

public class PersonalInfo : IPersonalInfo
{
    public PersonalInfo(string userName) => UserName = userName;

    public string UserName { get; }
    public string? Name { get; set; }
    public string? Email { get; set; }
}