using MediatR;
using TaskList.Contracts.Commands;
using TaskList.Contracts.Queries;

namespace TaskList.Services.Tests.Services;

public class ServiceTaskTests : IClassFixture<ServicesFixture>
{
    private readonly IMediator _mediator;
    private readonly Guid _notPossibleId;
    private readonly string _anyString;

    public ServiceTaskTests(ServicesFixture servicesFixture)
    {
        _mediator = servicesFixture.Mediator;
        _notPossibleId = new Guid("F88C636F-EA8D-424A-887C-C5683410A6B7");
        _anyString = "AD0AB380-AD46-4BC2-962B-16B152E6100E";
    }
    
    #region Commands
    
    [Theory]
    [InlineData("0000", "0000")]
    [InlineData("1111", "1111")]
    public async void CmdTaskCreate_Success(string name, string desc)
    {
        var createdTaskList = await _mediator.Send(new CommandTaskListCreate(_anyString, _anyString));
        
        var createdTask = await _mediator.Send(new CommandTaskCreate(name, desc, createdTaskList.Id));
        
        Assert.Equal(name, createdTask.Name);
        Assert.Equal(desc, createdTask.Description);
    }

    [Theory]
    [InlineData("0000", "0000")]
    [InlineData("1111", "1111")]
    public async void CmdTaskUpdate_Success(string name, string desc)
    {
        var createdTaskList = await _mediator.Send(new CommandTaskListCreate(_anyString, _anyString));
        var createdTask = await _mediator.Send(new CommandTaskCreate(_anyString, _anyString, createdTaskList.Id));
        
        var updatedString = await _mediator.Send(new CommandTaskUpdate(createdTask.Id, name, desc));
        
        Assert.Equal(name, updatedString.Name);
        Assert.Equal(desc, updatedString.Description);
    }

    [Fact]
    public async void CmdTaskUpdate_NotFound()
    {
        await Assert.ThrowsAsync<Ardalis.GuardClauses.NotFoundException>(async () => 
            await _mediator.Send(new CommandTaskUpdate(_notPossibleId, _anyString, _anyString)));
    }

    [Fact]
    public async void CmdTaskChangeTaskList_NotFound()
    {
        var createdTaskList = await _mediator.Send(new CommandTaskListCreate(_anyString, _anyString));
        
        var createdTask = await _mediator.Send(new CommandTaskCreate(_anyString, _anyString, createdTaskList.Id));
        
        await Assert.ThrowsAsync<Ardalis.GuardClauses.NotFoundException>(async () =>
            await _mediator.Send(new CommandTaskChangeTaskList(createdTask.Id, _notPossibleId)));
    }

    [Fact]
    public async void CmdTaskChangeTaskList_Success()
    {
        var createdTaskList = await _mediator.Send(new CommandTaskListCreate(_anyString, _anyString));
        var createdTask = await _mediator.Send(new CommandTaskCreate(_anyString, _anyString, createdTaskList.Id));
        var createdTaskListAnother = await _mediator.Send(new CommandTaskListCreate(_anyString, _anyString));
        
        var replacedTask = await _mediator.Send(new CommandTaskChangeTaskList(createdTask.Id, createdTaskListAnother.Id));
        
        Assert.Equal(createdTaskListAnother.Id, replacedTask.TaskListId);
    }

    [Fact]
    public async void CmdTaskDelete_Success()
    {
        var createdTaskList = await _mediator.Send(new CommandTaskListCreate(_anyString, _anyString));
        var createdTask = await _mediator.Send(new CommandTaskCreate(_anyString, _anyString, createdTaskList.Id));

        await _mediator.Send(new CommandTaskDelete(createdTask.Id));
        
        Assert.True(true);
    }

    [Fact]
    public async void CmdTaskDelete_NotFound()
    {
        await Assert.ThrowsAsync<Ardalis.GuardClauses.NotFoundException>(async () =>
            await _mediator.Send(new CommandTaskDelete(_notPossibleId)));
    }

    [Fact]
    public async void CmdTaskCreate_NotFoundTaskList()
    {
        await Assert.ThrowsAsync<Ardalis.GuardClauses.NotFoundException>(async () =>
            await _mediator.Send(new CommandTaskCreate(_anyString, _anyString, _notPossibleId)));
    }
    
    #endregion

    #region Queries
    
    [Theory]
    [InlineData("0000", "0000")]
    [InlineData("1111", "1111")]
    public async void QueryTaskGet_Success(string name, string desc)
    {
        var createdTaskList = await _mediator.Send(new CommandTaskListCreate(_anyString, _anyString));
        var createdTask = await _mediator.Send(new CommandTaskCreate(_anyString, _anyString, createdTaskList.Id));
        
        var responseTask = await _mediator.Send(new QueryTaskGet(createdTask.Id));
        
        Assert.Equal(name, responseTask.Name);
        Assert.Equal(desc, responseTask.Description);
    }
    
    [Fact]
    public async void QueryTaskGet_NotFound()
    {
        await Assert.ThrowsAsync<Ardalis.GuardClauses.NotFoundException>(async () =>
            await _mediator.Send(new QueryTaskGet(_notPossibleId)));
    }
    
    [Fact]
    public async void QueryTaskGetAll_Success()
    {
        var createdTaskList = await _mediator.Send(new CommandTaskListCreate(_anyString, _anyString));
        await _mediator.Send(new CommandTaskCreate(_anyString, _anyString, createdTaskList.Id));
        
        var responseTasks = await _mediator.Send(new QueryTaskGetAll());
        
        Assert.True(responseTasks.Any());
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(5)]
    public async void QueryTaskGetAllByTask_Success(int count)
    {
        var createdTaskList = await _mediator.Send(new CommandTaskListCreate(_anyString, _anyString));
        for (var i = 0; i < count; i++)
            await _mediator.Send(new CommandTaskCreate(_anyString, _anyString, createdTaskList.Id));

        var responseTasks = await _mediator.Send(new QueryTaskGetAllByTaskList(createdTaskList.Id));
        
        Assert.Equal(count, responseTasks.Count());
    }
    
    [Fact]
    public async void QueryTaskGetAllByTask_NotFound()
    {
        await Assert.ThrowsAsync<Ardalis.GuardClauses.NotFoundException>(async () =>
            await _mediator.Send(new QueryTaskGetAllByTaskList(_notPossibleId)));
    }

    #endregion
}