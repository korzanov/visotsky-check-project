using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TaskList.Presentation.Tests.Helpers;

public class ControllerIdentityHelper
{
    /// <summary>
    /// Set HttpContext with Identity to controller
    /// </summary>
    /// <param name="controller"></param>
    /// <param name="userIdentityName">value for controller.User.Identity.Name</param>
    internal static void SetHttpContextWithIdentity(ControllerBase controller, string userIdentityName)
    {
        controller.ControllerContext = new ControllerContext
        {
            HttpContext = HttpContextWithFakeUser(userIdentityName)
        };
    }
    
    private static ClaimsIdentity IdentityNameFake(string name)
    {
        Mock<ClaimsIdentity> mock = new();
        mock.Setup(c => c.Name).Returns(name);
        return mock.Object;
    }

    private static ClaimsPrincipal UserPrincipalFake(string identityName)
    {
        Mock<ClaimsPrincipal> mock = new();
        mock.Setup(c => c.Identity).Returns(IdentityNameFake(identityName));
        return mock.Object;
    }

    private static HttpContext HttpContextWithFakeUser(string userIdentityName)
    {
        Mock<HttpContext> mock = new();
        mock.Setup(c => c.User).Returns(UserPrincipalFake(userIdentityName));
        return mock.Object;
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("test")]
    public void TestMock(string userIdString)
    {
        var httpContext = HttpContextWithFakeUser(userIdString);
        
        Assert.NotNull(httpContext.User.Identity);
        Assert.Equal(userIdString, httpContext.User.Identity?.Name);
    }
}