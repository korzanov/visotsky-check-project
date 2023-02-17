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
[Route("/security/createToken")]
public class SecurityController : ControllerBase
{
    private readonly IServiceManager _serviceManager;
    private readonly Jwt _jwt;
    
    public SecurityController(IConfiguration configuration, IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
        _jwt = new Jwt(configuration);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> CreateToken([FromBody] UserAuthDto userAuth, CancellationToken cancellationToken = default)
    {
        var authResult = await _serviceManager.AuthService.AuthAsync(userAuth, cancellationToken);
        if (!authResult) 
            return Unauthorized();
        var token = _jwt.CreateAndWriteToken(userAuth);
        return Ok(token);
    }

    private class Jwt
    {
        internal readonly string Issuer;
        internal readonly string Audience;
        internal readonly byte[] Key;
        internal readonly TimeSpan Ttl;
        
        internal Jwt(IConfiguration configuration)
        {
            if (configuration is null) throw new ArgumentNullException(nameof(configuration));
            Issuer = configuration["Jwt:Issuer"] ?? throw new NullReferenceException();
            Audience = configuration["Jwt:Audience"] ?? throw new NullReferenceException();
            Key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"] ?? throw new NullReferenceException());
            var ttlSecondsString = configuration["Jwt:TimeToLiveInSeconds"] ?? throw new NullReferenceException();
            var ttlSeconds = int.Parse(ttlSecondsString);
            Ttl = TimeSpan.FromSeconds(ttlSeconds);
        }

        internal string CreateAndWriteToken(UserAuthDto userAuth)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Name, userAuth.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.Add(Ttl),
                Issuer = Issuer,
                Audience = Audience,
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(Key),
                    SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}