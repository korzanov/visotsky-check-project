using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TaskList.DbInfrastructure.Identity;

public class TaskListIdentityDbContext : IdentityDbContext<TaskListAppUser>
{
    //public TaskListIdentityDbContext(DbContextOptions options) : base(options) { }
    public TaskListIdentityDbContext(DbContextOptions<TaskListIdentityDbContext> options) : base(options) { }
}