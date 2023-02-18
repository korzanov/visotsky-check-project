using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskList.Contracts;
using TaskList.Services.Abstractions;

namespace TaskList.Presentation.Controllers;

[ApiController]
[Authorize]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    
    public UserController(IServiceManager serviceManager)
    {
        _userService = serviceManager.UserService;
    }
    
    [HttpPut("{userId:guid}")]
    public async Task<IActionResult> UpdateUser(Guid userId, [FromBody] UserUpdateDto userForUpdateDto, CancellationToken cancellationToken = default)
    {
        if (IsNotCurrentUser(userId))
            return Forbid();
        await _userService.UpdateAsync(userId, userForUpdateDto, cancellationToken);
        return NoContent();
    }
    
    [HttpDelete("{userId:guid}")]
    public async Task<IActionResult> DeleteUser(Guid userId, CancellationToken cancellationToken = default)
    {
        if (IsNotCurrentUser(userId))
            return Forbid();
        await _userService.DeleteAsync(userId, cancellationToken);
        return NoContent();
    }
    
    private bool IsNotCurrentUser(Guid userId)
    {
        return Guid.TryParse(User.Identity?.Name, out var currentUserId) && userId == currentUserId;
    }
}