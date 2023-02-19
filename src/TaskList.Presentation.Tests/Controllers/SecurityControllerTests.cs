using Microsoft.AspNetCore.Mvc;
using TaskList.Contracts.Queries;
using TaskList.Contracts.Responses;
using TaskList.Presentation.Controllers;
using TaskList.Presentation.Tests.Helpers;

namespace TaskList.Presentation.Tests.Controllers;

public class SecurityControllerTests : BaseControllerTests
{
    private readonly SecurityController _controller;
    
    private readonly AuthQuery _validUserAuthQuery;
    private readonly AuthQuery _invalidUserAuthQuery;

    public SecurityControllerTests()
    {
        var configuration = EnvironmentHelper.GetFakeConfigurationWithJwt();
        _controller = new SecurityController(MediatorMock.Object, configuration);
        
        _validUserAuthQuery = new AuthQuery("admin", "admin");
        var validUserId = new Guid("724C5832-737A-4F7A-8BF8-F115CD134D17");
        
        _invalidUserAuthQuery = new AuthQuery("admin", "password");
        var invalidUserId = new Guid("25195864-1D7E-4955-B616-8B76FFA5964B");

        SetupMediatrMockInRequests(new AuthResponse(true, validUserId), _validUserAuthQuery);
        SetupMediatrMockNotInRequests(new AuthResponse(false, invalidUserId), _validUserAuthQuery);
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