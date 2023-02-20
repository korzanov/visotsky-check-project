using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskList.Contracts.Commands;
using TaskList.Contracts.Queries;
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
    
    [HttpGet("{login}")]
    public async Task<IActionResult> GetUser(string login, CancellationToken cancellationToken = default)
    {
        if (await IsNotCurrentUser(login))
            return Forbid();
        var user = await _mediator.Send(new QueryPersonalInfoGet(login), cancellationToken);
        return Ok(user);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> CreateUser([FromBody] CommandPersonalInfoCreate personalInfoCreate, CancellationToken cancellationToken = default)
    {
        var user = await _mediator.Send(personalInfoCreate, cancellationToken);
        return CreatedAtAction(nameof(UserController.GetUser),  user.Login, user);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateUser(CommandPersonalInfoUpdate commandPersonalInfoUpdate, CancellationToken cancellationToken = default)
    {
        if (await IsNotCurrentUser(commandPersonalInfoUpdate.Login))
            return Forbid();
        var user = await _mediator.Send(commandPersonalInfoUpdate, cancellationToken);
        return Ok(user);
    }
    
    [HttpDelete("{login}")]
    public async Task<IActionResult> DeleteUser(string login, CancellationToken cancellationToken = default)
    {
        if (await IsNotCurrentUser(login))
            return Forbid();
        await _mediator.Send(new CommandPersonalInfoDelete(login), cancellationToken);
        return NoContent();
    }
    
    private async Task<bool> IsNotCurrentUser(string login)
    {
        var user = await _userManager.FindByNameAsync(User.Identity?.Name);
        return user is null || user.UserName != login;
    }
}