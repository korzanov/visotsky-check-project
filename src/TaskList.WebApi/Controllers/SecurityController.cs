using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TaskList.Contracts.Queries;
using TaskList.DbInfrastructure.Identity;

namespace TaskList.WebApi.Controllers;

[ApiController]
public class SecurityController : ControllerBase
{
    private readonly Jwt _jwt;
    private readonly UserManager<TaskListAppUser> _userManager;

    public SecurityController(IConfiguration configuration, UserManager<TaskListAppUser> userManager)
    {
        _userManager = userManager;
        _jwt = new Jwt(configuration);
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
        
        var token = _jwt.CreateAndWriteToken(user.UserName);
        return Ok(token);
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("/security/createUser")]
    public async Task<IActionResult> CreateUser([FromBody] QueryAuth queryAuth, CancellationToken cancellationToken = default)
    {
        var user = new TaskListAppUser(queryAuth.Login);

        var result = await _userManager.CreateAsync(user, queryAuth.Password);
        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok(queryAuth);
    }
    
    private class Jwt
    {
        private readonly string _issuer;
        private readonly string _audience;
        private readonly byte[] _key;
        private readonly TimeSpan _ttl;
        
        internal Jwt(IConfiguration configuration)
        {
            if (configuration is null) throw new ArgumentNullException(nameof(configuration));
            _issuer = configuration["Jwt:Issuer"] ?? throw new NullReferenceException();
            _audience = configuration["Jwt:Audience"] ?? throw new NullReferenceException();
            _key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"] ?? throw new NullReferenceException());
            var ttlSecondsString = configuration["Jwt:TimeToLiveInSeconds"] ?? throw new NullReferenceException();
            var ttlSeconds = int.Parse(ttlSecondsString);
            _ttl = TimeSpan.FromSeconds(ttlSeconds);
        }

        internal string CreateAndWriteToken(string userName)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Name, userName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.Add(_ttl),
                Issuer = _issuer,
                Audience = _audience,
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(_key),
                    SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}