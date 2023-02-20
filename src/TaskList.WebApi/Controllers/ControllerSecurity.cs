using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TaskList.Contracts.Queries;
using TaskList.DbInfrastructure.Identity;
using TaskList.WebApi.Security;

namespace TaskList.WebApi.Controllers;

[ApiController]
public class ControllerSecurity : ControllerBase
{
    private readonly JwtConfig _jwtConfig;
    private readonly UserManager<TaskListAppUser> _userManager;

    public ControllerSecurity(UserManager<TaskListAppUser> userManager, JwtConfig jwtConfig)
    {
        _userManager = userManager;
        _jwtConfig = jwtConfig;
    }

    [HttpGet]
    [Authorize]
    [Route("/security/checkToken")]
    public Task<IActionResult> CheckToken()
    {
        return Task.FromResult<IActionResult>(Ok());
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("/security/createToken")]
    public async Task<IActionResult> CreateToken([FromBody] QueryAuth queryAuth, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByNameAsync(queryAuth.Login);
        if (user is null)
            return Unauthorized();
        if(!await _userManager.CheckPasswordAsync(user, queryAuth.Password))
            return Unauthorized("invalid password");
        
        var token = _jwtConfig.CreateAndWriteToken(user.UserName);
        return Ok(token);
    }
}