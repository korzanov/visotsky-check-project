using Microsoft.AspNetCore.Mvc;
using TaskList.Contracts.Commands;
using TaskList.Contracts.Responses;
using TaskList.Presentation.Controllers;
using TaskList.Presentation.Tests.Helpers;

namespace TaskList.Presentation.Tests.Controllers;

public class UserControllerTests : BaseControllerTests
{
    private readonly UserController _userController;
    private readonly Guid _userIdOnContext;
    private readonly Guid _invalidUserId;

    public UserControllerTests()
    {
        _userIdOnContext = new Guid("282FB48E-F4F9-4A99-BF69-54D4D137F379");
        _invalidUserId = new Guid("FCBD4B5E-D3C2-42C9-97E6-F77ED2897068");

        _userController = new UserController(Mediator);
        ControllerIdentityHelper.SetHttpContextWithIdentity(_userController,_userIdOnContext.ToString());

        SetupMediatrMockAnyRequest<UpdateUserCommand, UserResponse>(new UserResponse());
        SetupMediatrMockAnyRequest<DeleteUserCommand>();
    }
    
    [Fact]
    public async void UserUpdate_Success()
    {
        var result = await _userController.UpdateUser(new UpdateUserCommand(_userIdOnContext/*, "new_name", "new_email"*/));
        
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async void UserUpdate_Forbidden()
    {
        var result = await _userController.UpdateUser(new UpdateUserCommand(_invalidUserId/*"new_name", "new_email"*/));
        
        Assert.IsType<ForbidResult>(result);
    }
    
    [Fact]
    public async void UserDelete_Success()
    {
        var result = await _userController.DeleteUser(_userIdOnContext);
        
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async void UserDelete_Forbidden()
    {
        var result = await _userController.DeleteUser(_invalidUserId);
        
        Assert.IsType<ForbidResult>(result);
    }
}