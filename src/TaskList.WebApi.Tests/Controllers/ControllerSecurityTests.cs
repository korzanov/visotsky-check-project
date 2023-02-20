using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskList.Contracts.Commands;
using TaskList.Contracts.Queries;
using TaskList.DbInfrastructure.Identity;
using TaskList.WebApi.Controllers;
using TaskList.WebApi.Tests.Helpers;

namespace TaskList.WebApi.Tests.Controllers;

public class ControllerSecurityTests
{
    private readonly ControllerSecurity _controller;
    
    private readonly QueryAuth _validUserQueryAuth;
    private readonly QueryAuth _invalidUserQueryAuth;

    public ControllerSecurityTests()
    {
        var configuration = HelperEnvironment.GetFakeConfigurationWithJwt();
        Mock<UserManager<TaskListAppUser>> mock = new();
        
        _validUserQueryAuth = new QueryAuth("admin", "admin");
        var validUser = new TaskListAppUser(_validUserQueryAuth.Login);
        
        _invalidUserQueryAuth = new QueryAuth("user", "password");
        var invalidUser = new TaskListAppUser(_validUserQueryAuth.Login);

        mock.Setup(m => m.FindByNameAsync(It.IsIn(_validUserQueryAuth.Login)))
            .Returns(Task.FromResult(new TaskListAppUser(_validUserQueryAuth.Login)));
        
        mock.Setup(m => m.CheckPasswordAsync(It.IsIn(validUser), _validUserQueryAuth.Password))
            .Returns(Task.FromResult(true));
        mock.Setup(m => m.CheckPasswordAsync(It.IsIn(validUser), It.IsNotIn(_validUserQueryAuth.Password)))
            .Returns(Task.FromResult(false));
        mock.Setup(m => m.CreateAsync(It.IsAny<TaskListAppUser>(), _validUserQueryAuth.Password))
            .Returns(Task.FromResult(IdentityResult.Success));
        
        _controller = new ControllerSecurity(configuration, mock.Object);
    }

    [Fact]
    public async void CreateToken_Ok()
    {
        var result = await _controller.CreateToken(_validUserQueryAuth);
        var okObjectResult = Assert.IsAssignableFrom<OkObjectResult>(result);
        Assert.IsType<string>(okObjectResult.Value);
    }
    
    [Fact]
    public async void CreateToken_Unauthorized()
    {
        var result = await _controller.CreateToken(_invalidUserQueryAuth);
        Assert.IsAssignableFrom<UnauthorizedResult>(result);
    }
}