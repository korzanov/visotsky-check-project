using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskList.Contracts.Queries;
using TaskList.DbInfrastructure.Identity;
using TaskList.WebApi.Controllers;
using TaskList.WebApi.Tests.Helpers;

namespace TaskList.WebApi.Tests.Controllers;

public class SecurityControllerTests
{
    private readonly SecurityController _controller;
    
    private readonly AuthQuery _validUserAuthQuery;
    private readonly AuthQuery _invalidUserAuthQuery;

    public SecurityControllerTests()
    {
        var configuration = EnvironmentHelper.GetFakeConfigurationWithJwt();
        Mock<UserManager<TaskListAppUser>> mock = new();
        
        _validUserAuthQuery = new AuthQuery("admin", "admin");
        var validUser = new TaskListAppUser(_validUserAuthQuery.Login);
        
        _invalidUserAuthQuery = new AuthQuery("user", "password");
        var invalidUser = new TaskListAppUser(_validUserAuthQuery.Login);

        mock.Setup(m => m.FindByNameAsync(It.IsIn(_validUserAuthQuery.Login)))
            .Returns(Task.FromResult(new TaskListAppUser(_validUserAuthQuery.Login)));
        
        mock.Setup(m => m.CheckPasswordAsync(It.IsIn(validUser), _validUserAuthQuery.Password))
            .Returns(Task.FromResult(true));
        mock.Setup(m => m.CheckPasswordAsync(It.IsIn(validUser), It.IsNotIn(_validUserAuthQuery.Password)))
            .Returns(Task.FromResult(false));
        mock.Setup(m => m.CreateAsync(It.IsAny<TaskListAppUser>(), _validUserAuthQuery.Password))
            .Returns(Task.FromResult(IdentityResult.Success));
        
        _controller = new SecurityController(configuration, mock.Object);
    }

    [Fact]
    public async void CreateToken_Ok()
    {
        var result = await _controller.CreateToken(_validUserAuthQuery);
        var okObjectResult = Assert.IsAssignableFrom<OkObjectResult>(result);
        Assert.IsType<string>(okObjectResult.Value);
    }
    
    [Fact]
    public async void CreateToken_Unauthorized()
    {
        var result = await _controller.CreateToken(_invalidUserAuthQuery);
        Assert.IsAssignableFrom<UnauthorizedResult>(result);
    }
}