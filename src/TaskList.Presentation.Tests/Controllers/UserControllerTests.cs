using Microsoft.AspNetCore.Mvc;
using TaskList.Contracts;
using TaskList.Presentation.Controllers;
using TaskList.Presentation.Tests.Helpers;
using TaskList.Services.Abstractions;

namespace TaskList.Presentation.Tests.Controllers;

public class UserControllerTests : BaseControllerTests<UserController>
{
    private readonly UserController _userController;
    private readonly Guid _userIdOnContext;

    public UserControllerTests()
    {
        _userIdOnContext = new Guid("282FB48E-F4F9-4A99-BF69-54D4D137F379");

        Mock<IUserService> userServiceMock = new();
        ServiceManagerMock.Setup(m => m.UserService).Returns(userServiceMock.Object);
        _userController = new UserController(ServiceManager);
        ControllerIdentityHelper.SetHttpContextWithIdentity(_userController,_userIdOnContext.ToString());

        userServiceMock
            .Setup(userService => userService
                .UpdateAsync(_userIdOnContext, It.IsAny<UserUpdateDto>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        
        userServiceMock
            .Setup(userService => userService
                .DeleteAsync(_userIdOnContext, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
    }
    
    [Fact]
    public async void UserUpdate_Success()
    {
        var result = await _userController
            .UpdateUser(_userIdOnContext, new UserUpdateDto("new_name", "new_email"));
        
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async void UserUpdate_Forbidden()
    {
        var result = await _userController
            .UpdateUser(new Guid("FCBD4B5E-D3C2-42C9-97E6-F77ED2897068"), new UserUpdateDto("new_name", "new_email"));
        
        Assert.IsType<ForbidResult>(result);
    }
    
    [Fact]
    public async void UserDelete_Success()
    {
        var result = await _userController
            .DeleteUser(_userIdOnContext);
        
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async void UserDelete_Forbidden()
    {
        var result = await _userController
            .DeleteUser(new Guid("FCBD4B5E-D3C2-42C9-97E6-F77ED2897068"));
        
        Assert.IsType<ForbidResult>(result);
    }
}