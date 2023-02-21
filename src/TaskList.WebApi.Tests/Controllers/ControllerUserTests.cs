using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskList.Contracts.Commands;
using TaskList.Contracts.Responses;
using TaskList.DbInfrastructure.Identity;
using TaskList.WebApi.Controllers;
using TaskList.WebApi.Tests.Helpers;

namespace TaskList.WebApi.Tests.Controllers;

public class ControllerUserTests : ControllerWithMediatorTests
{
    private readonly ControllerUser _controllerUser;
    private readonly string _loginOnContext;
    private readonly string _invalidLogin;

    public ControllerUserTests()
    {
        Mock<UserManager<TaskListAppUser>> mock = new();
        
        _loginOnContext = "admin";
        _invalidLogin = "user";

        _controllerUser = new ControllerUser(Mediator, mock.Object);
        HelperControllerIdentity.SetHttpContextWithIdentity(_controllerUser,_loginOnContext);

        SetupMediatrMockAnyRequest<CommandPersonalInfoUpdate, ResponsePersonalInfo>(new Mock<ResponsePersonalInfo>().Object);
        SetupMediatrMockAnyRequest<CommandPersonalInfoDelete>();

        mock.Setup(m => m.FindByNameAsync(It.IsAny<string>()))
            .Returns(Task.FromResult(new TaskListAppUser(_loginOnContext)));
    }
    
    [Fact (Skip = "while identity not mocked")]
    public async void UserUpdate_Success()
    {
        var result = await _controllerUser.UpdateUser(new CommandPersonalInfoUpdate(_loginOnContext, "new_name", "new_email"));
        
        Assert.IsType<NoContentResult>(result);
    }

    [Fact (Skip = "while identity not mocked")]
    public async void UserUpdate_Forbidden()
    {
        var result = await _controllerUser.UpdateUser(new CommandPersonalInfoUpdate(_invalidLogin,"new_name", "new_email"));
        
        Assert.IsType<ForbidResult>(result);
    }
    
    [Fact (Skip = "while identity not mocked")]
    public async void UserDelete_Success()
    {
        var result = await _controllerUser.DeleteUser(new CommandPersonalInfoDelete(_loginOnContext));
        
        Assert.IsType<NoContentResult>(result);
    }

    [Fact (Skip = "while identity not mocked")]
    public async void UserDelete_Forbidden()
    {
        var result = await _controllerUser.DeleteUser(new CommandPersonalInfoDelete(_invalidLogin));
        
        Assert.IsType<ForbidResult>(result);
    }
}