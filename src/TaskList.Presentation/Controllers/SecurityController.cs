using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TaskList.Contracts;
using TaskList.Services.Abstractions;

namespace TaskList.Presentation.Controllers;

[ApiController]
public class SecurityController : ControllerBase
{
    private readonly IServiceManager _serviceManager;
    private readonly Jwt _jwt;
    private readonly ILogger<SecurityController> _logger;

    public SecurityController(IConfiguration configuration, IServiceManager serviceManager, ILogger<SecurityController> logger)
    {
        _serviceManager = serviceManager;
        _logger = logger;
        _jwt = new Jwt(configuration);
    }

    [HttpGet]
    [Authorize]
    [Route("/security/checkToken")]
    public async Task<IActionResult> CheckToken()
    {
        return Ok();
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("/security/createToken")]
    public async Task<IActionResult> CreateToken([FromBody] UserAuthDto userAuth, CancellationToken cancellationToken = default)
    {
        var authResult = await _serviceManager.AuthService.AuthAsync(userAuth, cancellationToken);
        if (!authResult) 
            return Unauthorized();
        var token = _jwt.CreateAndWriteToken(userAuth.UserName);
        _logger.LogDebug("Send new JWT token to \'{UserAuth}\'", userAuth);
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

        internal string CreateAndWriteToken(string userName)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", Guid.NewGuid().ToString()),
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