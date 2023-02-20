using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskList.Contracts.Commands;
using TaskList.Contracts.Responses;
using TaskList.DbInfrastructure.Identity;
using TaskList.WebApi.Controllers;
using TaskList.WebApi.Tests.Helpers;

namespace TaskList.WebApi.Tests.Controllers;

public class UserControllerTests : BaseControllerTests
{
    private readonly UserController _userController;
    private readonly string _loginOnContext;
    private readonly string _invalidLogin;

    public UserControllerTests()
    {
        Mock<UserManager<TaskListAppUser>> mock = new();
        
        _loginOnContext = "admin";
        _invalidLogin = "user";

        _userController = new UserController(Mediator, mock.Object);
        ControllerIdentityHelper.SetHttpContextWithIdentity(_userController,_loginOnContext);

        SetupMediatrMockAnyRequest<UpdatePersonalInfoCommand, PersonalInfoResponse>(new Mock<PersonalInfoResponse>().Object);
        SetupMediatrMockAnyRequest<DeletePersonalInfoCommand>();

        mock.Setup(m => m.FindByNameAsync(It.IsAny<string>()))
            .Returns(Task.FromResult(new TaskListAppUser(_loginOnContext)));
    }
    
    [Fact]
    public async void UserUpdate_Success()
    {
        var result = await _userController.UpdateUser(new UpdatePersonalInfoCommand(_loginOnContext, "new_name", "new_email"));
        
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async void UserUpdate_Forbidden()
    {
        var result = await _userController.UpdateUser(new UpdatePersonalInfoCommand(_invalidLogin,"new_name", "new_email"));
        
        Assert.IsType<ForbidResult>(result);
    }
    
    [Fact]
    public async void UserDelete_Success()
    {
        var result = await _userController.DeleteUser(_loginOnContext);
        
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async void UserDelete_Forbidden()
    {
        var result = await _userController.DeleteUser(_invalidLogin);
        
        Assert.IsType<ForbidResult>(result);
    }
}