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
[Route(RouteConstants.UriUsers)]
public class ControllerUser : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly UserManager<TaskListAppUser> _userManager;
    
    public ControllerUser(IMediator mediator, UserManager<TaskListAppUser> userManager)
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
        return CreatedAtAction(nameof(ControllerUser.GetUser),  new { login = user.Login }, user);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromBody] CommandPersonalInfoUpdate commandPersonalInfoUpdate, CancellationToken cancellationToken = default)
    {
        if (await IsNotCurrentUser(commandPersonalInfoUpdate.Login))
            return Forbid();
        var user = await _mediator.Send(commandPersonalInfoUpdate, cancellationToken);
        return Ok(user);
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteUser([FromBody] CommandPersonalInfoDelete delete, CancellationToken cancellationToken = default)
    {
        if (await IsNotCurrentUser(delete.Login))
            return Forbid();
        await _mediator.Send(delete, cancellationToken);
        return NoContent();
    }
    
    private async Task<bool> IsNotCurrentUser(string login)
    {
        var user = await _userManager.FindByNameAsync(User.Identity?.Name);
        return user is null || user.UserName != login;
    }
}