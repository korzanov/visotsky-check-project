using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskList.Contracts.Commands;

namespace TaskList.Presentation.Controllers;

[ApiController]
[Authorize]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateUser(UpdateUserCommand updateUserCommand, CancellationToken cancellationToken = default)
    {
        if (IsNotCurrentUser(updateUserCommand.Id))
            return Forbid();
        await _mediator.Send(updateUserCommand, cancellationToken);
        return NoContent();
    }
    
    [HttpDelete("{userId:guid}")]
    public async Task<IActionResult> DeleteUser(Guid userId, CancellationToken cancellationToken = default)
    {
        if (IsNotCurrentUser(userId))
            return Forbid();
        await _mediator.Send(new DeleteUserCommand(userId), cancellationToken);
        return NoContent();
    }
    
    private bool IsNotCurrentUser(Guid userId)
    {
        return !(Guid.TryParse(User.Identity?.Name, out var currentUserId) && userId == currentUserId);
    }
}