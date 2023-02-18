using Microsoft.AspNetCore.Mvc;
using TaskList.Contracts;
using TaskList.Presentation.Controllers;
using TaskList.Services.Abstractions;

namespace TaskList.Presentation.Tests.Controllers;

public class TaskListControllerTests : BaseControllerTests<TaskListController>
{
    private readonly TaskListController _controller;

    public TaskListControllerTests()
    {
        Mock<ITaskListService> mock = new();
        ServiceManagerMock.Setup(m => m.TaskListService).Returns(mock.Object);
        _controller = new TaskListController(ServiceManager);

        mock
            .Setup(s => s.GetAllAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(Enumerable.Empty<TaskListDto>()));

        mock
            .Setup(s => s.GetByIdAsync(It.IsAny<Guid>(),It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(new TaskListDto(Guid.NewGuid())));
        
        mock
            .Setup(s => s.CreateAsync(It.IsAny<TaskListCreateDto>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(new TaskListDto(Guid.NewGuid())));
        
        mock
            .Setup(s => s.UpdateAsync(It.IsAny<Guid>(), It.IsAny<TaskListUpdateDto>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        
        mock
            .Setup(s => s.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
    }

    [Fact]
    public async void GetAll_Success()
    {
        var result = await _controller.GetTaskLists();
        
        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.IsAssignableFrom<IEnumerable<TaskListDto>>(ok.Value);
    }

    [Fact]
    public async void Get_Success()
    {
        var result = await _controller.GetTaskList(Guid.NewGuid());
        
        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.IsAssignableFrom<TaskListDto>(ok.Value);
    }

    [Fact]
    public async void Create_Success()
    {
        var result = await _controller.CreateTaskList(new TaskListCreateDto());
        
        var ok = Assert.IsType<CreatedAtActionResult>(result);
        Assert.IsAssignableFrom<TaskListDto>(ok.Value);
    }

    [Fact]
    public async void Update_Success()
    {
        var result = await _controller.UpdateTaskList(Guid.NewGuid(), new TaskListUpdateDto());
        
        Assert.IsType<NoContentResult>(result);
    }
    
    [Fact]
    public async void Delete_Success()
    {
        var result = await _controller.DeleteTaskList(new Guid());
        
        Assert.IsType<NoContentResult>(result);
    }
}