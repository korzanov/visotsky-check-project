using Microsoft.AspNetCore.Mvc;
using TaskList.Contracts.Commands;
using TaskList.Contracts.Queries;
using TaskList.Contracts.Responses;
using TaskList.WebApi.Controllers;

namespace TaskList.WebApi.Tests.Controllers;

public class TaskListControllerTests : BaseControllerTests
{
    private readonly TaskListController _controller;

    public TaskListControllerTests()
    {
        _controller = new TaskListController(Mediator);

        var defaultTaskList = new TaskListResponse(Guid.Empty, null, null);
        
        SetupMediatrMockAnyRequest<GetTaskListsQuery, IEnumerable<TaskListResponse>>(Enumerable.Empty<TaskListResponse>());
        SetupMediatrMockAnyRequest<GetTaskListQuery, TaskListResponse>(defaultTaskList);
        SetupMediatrMockAnyRequest<CreateTaskListCommand, TaskListResponse>(defaultTaskList);
        SetupMediatrMockAnyRequest<UpdateTaskListCommand, TaskListResponse>(defaultTaskList);
        SetupMediatrMockAnyRequest<DeleteTaskListCommand>();
    }

    [Fact]
    public async void GetAll_Success()
    {
        var result = await _controller.GetTaskLists();
        
        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.IsAssignableFrom<IEnumerable<TaskListResponse>>(ok.Value);
    }

    [Fact]
    public async void Get_Success()
    {
        var result = await _controller.GetTaskList(Guid.NewGuid());
        
        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.IsAssignableFrom<TaskListResponse>(ok.Value);
    }

    [Fact]
    public async void Create_Success()
    {
        var result = await _controller.CreateTaskList(new CreateTaskListCommand(null,null));
        
        var ok = Assert.IsType<CreatedAtActionResult>(result);
        Assert.IsAssignableFrom<TaskListResponse>(ok.Value);
    }

    [Fact]
    public async void Update_Success()
    {
        var id = Guid.NewGuid();
        var result = await _controller.UpdateTaskList(new UpdateTaskListCommand(id, null,null));
        
        var ok = Assert.IsType<AcceptedAtActionResult>(result);
        Assert.IsAssignableFrom<TaskListResponse>(ok.Value);
    }
    
    [Fact]
    public async void Delete_Success()
    {
        var result = await _controller.DeleteTaskList(new Guid());
        
        Assert.IsType<NoContentResult>(result);
    }
}