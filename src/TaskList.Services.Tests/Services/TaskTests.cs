using TaskList.Contracts.Commands;
using TaskList.Contracts.Queries;

namespace TaskList.Services.Tests.Services;

public class TaskTests : ClassFixture
{
    public TaskTests(ServicesFixture servicesFixture) : base(servicesFixture) {}
    
    #region Commands
    
    [Theory]
    [InlineData("0000", "0000")]
    [InlineData("1111", "1111")]
    public async void CmdTaskCreate_Success(string name, string desc)
    {
        var createdTaskList = await Mediator.Send(new CommandTaskListCreate(AnyString, AnyString));
        
        var createdTask = await Mediator.Send(new CommandTaskCreate(name, desc, createdTaskList.Id));
        
        Assert.Equal(name, createdTask.Name);
        Assert.Equal(desc, createdTask.Description);
    }

    [Theory]
    [InlineData("0000", "0000")]
    [InlineData("1111", "1111")]
    public async void CmdTaskUpdate_Success(string name, string desc)
    {
        var createdTaskList = await Mediator.Send(new CommandTaskListCreate(AnyString, AnyString));
        var createdTask = await Mediator.Send(new CommandTaskCreate(AnyString, AnyString, createdTaskList.Id));
        
        var updatedString = await Mediator.Send(new CommandTaskUpdate(createdTask.Id, name, desc));
        
        Assert.Equal(name, updatedString.Name);
        Assert.Equal(desc, updatedString.Description);
    }

    [Fact]
    public async void CmdTaskUpdate_NotFound()
    {
        await Assert.ThrowsAsync<Ardalis.GuardClauses.NotFoundException>(async () => 
            await Mediator.Send(new CommandTaskUpdate(NotPossibleId, AnyString, AnyString)));
    }

    [Fact]
    public async void CmdTaskChangeTaskList_TaskList_NotFound()
    {
        var createdTaskList = await Mediator.Send(new CommandTaskListCreate(AnyString, AnyString));
        
        var createdTask = await Mediator.Send(new CommandTaskCreate(AnyString, AnyString, createdTaskList.Id));
        
        await Assert.ThrowsAsync<Ardalis.GuardClauses.NotFoundException>(async () =>
            await Mediator.Send(new CommandTaskChangeTaskList(createdTask.Id, NotPossibleId)));
    }

    [Fact]
    public async void CmdTaskChangeTaskList_Task_NotFound()
    {
        var createdTaskList = await Mediator.Send(new CommandTaskListCreate(AnyString, AnyString));
        
        await Assert.ThrowsAsync<Ardalis.GuardClauses.NotFoundException>(async () =>
            await Mediator.Send(new CommandTaskChangeTaskList(NotPossibleId, createdTaskList.Id)));
    }

    [Fact]
    public async void CmdTaskChangeTaskList_Success()
    {
        var createdTaskList = await Mediator.Send(new CommandTaskListCreate(AnyString, AnyString));
        var createdTask = await Mediator.Send(new CommandTaskCreate(AnyString, AnyString, createdTaskList.Id));
        var createdTaskListAnother = await Mediator.Send(new CommandTaskListCreate(AnyString, AnyString));
        
        var replacedTask = await Mediator.Send(new CommandTaskChangeTaskList(createdTask.Id, createdTaskListAnother.Id));
        
        Assert.Equal(createdTaskListAnother.Id, replacedTask.TaskListId);
    }

    [Fact]
    public async void CmdTaskDelete_Success()
    {
        var createdTaskList = await Mediator.Send(new CommandTaskListCreate(AnyString, AnyString));
        var createdTask = await Mediator.Send(new CommandTaskCreate(AnyString, AnyString, createdTaskList.Id));

        await Mediator.Send(new CommandTaskDelete(createdTask.Id));
        
        Assert.True(true);
    }

    [Fact]
    public async void CmdTaskDelete_NotFound()
    {
        await Assert.ThrowsAsync<Ardalis.GuardClauses.NotFoundException>(async () =>
            await Mediator.Send(new CommandTaskDelete(NotPossibleId)));
    }

    [Fact]
    public async void CmdTaskCreate_NotFoundTaskList()
    {
        await Assert.ThrowsAsync<Ardalis.GuardClauses.NotFoundException>(async () =>
            await Mediator.Send(new CommandTaskCreate(AnyString, AnyString, NotPossibleId)));
    }
    
    #endregion

    #region Queries
    
    [Theory]
    [InlineData("0000", "0000")]
    [InlineData("1111", "1111")]
    public async void QueryTaskGet_Success(string name, string desc)
    {
        var createdTaskList = await Mediator.Send(new CommandTaskListCreate(AnyString, AnyString));
        var createdTask = await Mediator.Send(new CommandTaskCreate(name, desc, createdTaskList.Id));
        
        var responseTask = await Mediator.Send(new QueryTaskGet(createdTask.Id));
        
        Assert.Equal(name, responseTask.Name);
        Assert.Equal(desc, responseTask.Description);
    }
    
    [Fact]
    public async void QueryTaskGet_NotFound()
    {
        await Assert.ThrowsAsync<Ardalis.GuardClauses.NotFoundException>(async () =>
            await Mediator.Send(new QueryTaskGet(NotPossibleId)));
    }
    
    [Fact]
    public async void QueryTaskGetAll_Success()
    {
        var createdTaskList = await Mediator.Send(new CommandTaskListCreate(AnyString, AnyString));
        await Mediator.Send(new CommandTaskCreate(AnyString, AnyString, createdTaskList.Id));
        
        var responseTasks = await Mediator.Send(new QueryTaskGetAll());
        
        Assert.True(responseTasks.Any());
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(5)]
    public async void QueryTaskGetAllByTask_Success(int count)
    {
        var createdTaskList = await Mediator.Send(new CommandTaskListCreate(AnyString, AnyString));
        for (var i = 0; i < count; i++)
            await Mediator.Send(new CommandTaskCreate(AnyString, AnyString, createdTaskList.Id));

        var responseTasks = await Mediator.Send(new QueryTaskGetAllByTaskList(createdTaskList.Id));
        
        Assert.Equal(count, responseTasks.Count());
    }
    
    [Fact]
    public async void QueryTaskGetAllByTask_NotFound()
    {
        await Assert.ThrowsAsync<Ardalis.GuardClauses.NotFoundException>(async () =>
            await Mediator.Send(new QueryTaskGetAllByTaskList(NotPossibleId)));
    }

    #endregion
}