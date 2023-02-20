using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace TaskList.WebApi.Security;

public sealed class JwtConfig
{
    private readonly string _issuer;
    private readonly string _audience;
    private readonly byte[] _key;
    private readonly TimeSpan _ttl;
        
    public JwtConfig(IConfiguration configuration)
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