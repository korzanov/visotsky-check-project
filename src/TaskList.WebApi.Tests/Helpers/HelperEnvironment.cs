using Microsoft.Extensions.Configuration;
using TaskList.WebApi.Security;

namespace TaskList.WebApi.Tests.Helpers;

internal static class HelperEnvironment
{
    internal static JwtConfig GetFakeJwtConfig() => new JwtConfig(GetFakeConfigurationWithJwt());

    private static IConfiguration GetFakeConfigurationWithJwt()
    {
        var configurationMock = new Mock<IConfiguration>();
        configurationMock.Setup(c => c["Jwt:Issuer"]).Returns("someIssuer");
        configurationMock.Setup(c => c["Jwt:Audience"]).Returns("someAudience");
        configurationMock.Setup(c => c["Jwt:Key"]).Returns("this is my custom Secret key for authentication");
        configurationMock.Setup(c => c["Jwt:TimeToLiveInSeconds"]).Returns(200.ToString());
        return configurationMock.Object;
    }
}