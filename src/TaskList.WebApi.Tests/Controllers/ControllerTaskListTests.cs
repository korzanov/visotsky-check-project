using Microsoft.AspNetCore.Mvc;
using TaskList.Contracts.Commands;
using TaskList.Contracts.Queries;
using TaskList.Contracts.Responses;
using TaskList.WebApi.Controllers;

namespace TaskList.WebApi.Tests.Controllers;

public class ControllerTaskListTests : ControllerWithMediatorTests
{
    private readonly ControllerTaskList _controller;

    public ControllerTaskListTests()
    {
        _controller = new ControllerTaskList(Mediator);

        var defaultTaskList = new ResponseTaskList(Guid.Empty, null, null);
        
        SetupMediatrMockAnyRequest<QueryTaskListGetAll, IEnumerable<ResponseTaskList>>(Enumerable.Empty<ResponseTaskList>());
        SetupMediatrMockAnyRequest<QueryTaskListGet, ResponseTaskList>(defaultTaskList);
        SetupMediatrMockAnyRequest<CommandTaskListCreate, ResponseTaskList>(defaultTaskList);
        SetupMediatrMockAnyRequest<CommandTaskListUpdate, ResponseTaskList>(defaultTaskList);
        SetupMediatrMockAnyRequest<CommandTaskListDelete>();
    }

    [Fact]
    public async void GetAll_Success()
    {
        var result = await _controller.GetTaskLists();
        
        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.IsAssignableFrom<IEnumerable<ResponseTaskList>>(ok.Value);
    }

    [Fact]
    public async void Get_Success()
    {
        var result = await _controller.GetTaskList(Guid.NewGuid());
        
        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.IsAssignableFrom<ResponseTaskList>(ok.Value);
    }

    [Fact]
    public async void Create_Success()
    {
        var result = await _controller.CreateTaskList(new CommandTaskListCreate(null,null));
        
        var ok = Assert.IsType<CreatedAtActionResult>(result);
        Assert.IsAssignableFrom<ResponseTaskList>(ok.Value);
    }

    [Fact]
    public async void Update_Success()
    {
        var id = Guid.NewGuid();
        var result = await _controller.UpdateTaskList(new CommandTaskListUpdate(id, null,null));
        
        var ok = Assert.IsType<CreatedAtActionResult>(result);
        Assert.IsAssignableFrom<ResponseTaskList>(ok.Value);
    }
    
    [Fact]
    public async void Delete_Success()
    {
        var result = await _controller.DeleteTaskList(new Guid());
        
        Assert.IsType<NoContentResult>(result);
    }
}