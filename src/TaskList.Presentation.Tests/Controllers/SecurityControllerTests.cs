using Microsoft.AspNetCore.Mvc;
using TaskList.Contracts;
using TaskList.Presentation.Controllers;
using TaskList.Presentation.Tests.Helpers;
using TaskList.Services.Abstractions;

namespace TaskList.Presentation.Tests.Controllers;

public class SecurityControllerTests : BaseControllerTests<SecurityController>
{
    private readonly SecurityController _controller;
    private readonly UserAuthDto _validUserAuthDto;
    private readonly UserAuthDto _invalidUserAuthDto;

    public SecurityControllerTests()
    {
        Mock<IAuthService> authServiceMock = new();
        ServiceManagerMock.Setup(manager => manager.AuthService).Returns(authServiceMock.Object);
        
        var configuration = EnvironmentHelper.GetFakeConfigurationWithJwt();
        _controller = new SecurityController(configuration, ServiceManager, Logger);
        
        _validUserAuthDto = new UserAuthDto("admin", "admin");
        _invalidUserAuthDto = new UserAuthDto("admin", "password");
        
        authServiceMock
            .Setup(service => service.AuthAsync(It.IsIn(_validUserAuthDto), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        authServiceMock
            .Setup(service => service.AuthAsync(It.IsNotIn(_validUserAuthDto), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
    }

    [Fact]
    public async void CreateToken_Ok()
    {
        var result = await _controller.CreateToken(_validUserAuthDto);
        var okObjectResult = Assert.IsAssignableFrom<OkObjectResult>(result);
        Assert.IsType<string>(okObjectResult.Value);
    }
    
    [Fact]
    public async void CreateToken_Unauthorized()
    {
        var result = await _controller.CreateToken(_invalidUserAuthDto);
        Assert.IsAssignableFrom<UnauthorizedResult>(result);
    }
}