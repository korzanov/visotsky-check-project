using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskList.Contracts.Commands;
using TaskList.DbInfrastructure.Identity;

namespace TaskList.WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly UserManager<TaskListAppUser> _userManager;
    
    public UserController(IMediator mediator, UserManager<TaskListAppUser> userManager)
    {
        _mediator = mediator;
        _userManager = userManager;
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateUser(UpdateUserCommand updateUserCommand, CancellationToken cancellationToken = default)
    {
        if (await IsNotCurrentUser(updateUserCommand.Login))
            return Forbid();
        var user = await _mediator.Send(updateUserCommand, cancellationToken);
        return Ok(user);
    }
    
    [HttpDelete("{login}")]
    public async Task<IActionResult> DeleteUser(string login, CancellationToken cancellationToken = default)
    {
        if (await IsNotCurrentUser(login))
            return Forbid();
        await _mediator.Send(new DeleteUserCommand(login), cancellationToken);
        return NoContent();
    }
    
    private async Task<bool> IsNotCurrentUser(string login)
    {
        var user = await _userManager.FindByNameAsync(User.Identity?.Name);
        return user is null || user.UserName != login;
    }
}