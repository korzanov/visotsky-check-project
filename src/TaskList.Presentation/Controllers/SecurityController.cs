using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TaskList.Contracts.Queries;

namespace TaskList.Presentation.Controllers;

[ApiController]
public class SecurityController : ControllerBase
{
    private readonly Jwt _jwt;
    private readonly IMediator _mediator;

    public SecurityController(IMediator mediator, IConfiguration configuration)
    {
        _mediator = mediator;
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
    public async Task<IActionResult> CreateToken([FromBody] AuthQuery auth, CancellationToken cancellationToken = default)
    {
        var authResponse = await _mediator.Send(auth, cancellationToken);
        if (!authResponse.Success) 
            return Unauthorized();
        var token = _jwt.CreateAndWriteToken(authResponse.UserId);
        return Ok(token);
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

        internal string CreateAndWriteToken(Guid userId)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Name, userId.ToString()),
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