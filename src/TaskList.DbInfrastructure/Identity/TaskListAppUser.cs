using Microsoft.AspNetCore.Identity;
using TaskList.Domain.Entities;

namespace TaskList.DbInfrastructure.Identity;

public sealed class TaskListAppUser : IdentityUser, IPersonalInfo
{
    private TaskListAppUser() { }
    public TaskListAppUser(string login) : base(login) { }

    public string Login => UserName;
    public string? Name { get; set; }
}