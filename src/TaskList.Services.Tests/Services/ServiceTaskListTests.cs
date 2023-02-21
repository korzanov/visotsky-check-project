using MediatR;
using TaskList.Contracts.Commands;
using TaskList.Contracts.Queries;

namespace TaskList.Services.Tests.Services;

public class ServiceTaskListTests : IClassFixture<ServicesFixture>
{
    private readonly IMediator _mediator;
    private readonly Guid _notPossibleTaskListId;
    private readonly string _anyString;
    
    public ServiceTaskListTests(ServicesFixture servicesFixture)
    {
        _mediator = servicesFixture.Mediator;
        _notPossibleTaskListId = new Guid("F88C636F-EA8D-424A-887C-C5683410A6B7");
        _anyString = "AD0AB380-AD46-4BC2-962B-16B152E6100E";
    }
    
    [Theory]
    [InlineData("00000", "000000")]
    [InlineData("11111", "111111")]
    public async void CommandTaskListCreate_Success(string name, string description)
    {
        var responseTaskList = await _mediator.Send(new CommandTaskListCreate(name, description));
        
        Assert.Equal(name, responseTaskList.Name);
        Assert.Equal(description, responseTaskList.Description);
    }
    
    [Theory]
    [InlineData("00000", "000000")]
    [InlineData("11111", "111111")]
    public async void CommandTaskListUpdate_Success(string name, string description)
    {
        var createdTaskList = await _mediator.Send(new CommandTaskListCreate(_anyString, _anyString));
        
        var responseTaskList = await _mediator.Send(new CommandTaskListUpdate(createdTaskList.Id, name, description));
        
        Assert.Equal(name, responseTaskList.Name);
        Assert.Equal(description, responseTaskList.Description);
    }

    [Fact]
    public async void CommandTaskListUpdate_Throws()
    {
        await Assert.ThrowsAnyAsync<Exception>(async () =>
            await _mediator.Send(new CommandTaskListUpdate(_notPossibleTaskListId, _anyString, _anyString)));
        await Assert.ThrowsAsync<Ardalis.GuardClauses.NotFoundException>(async () =>
            await _mediator.Send(new CommandTaskListUpdate(_notPossibleTaskListId, _anyString, _anyString)));
    }

    [Fact]
    public async void CommandTaskListDelete_Success()
    {
        var createdTaskList = await _mediator.Send(new CommandTaskListCreate(_anyString, _anyString));
        await _mediator.Send(new CommandTaskListDelete(createdTaskList.Id));

        await Assert.ThrowsAsync<Ardalis.GuardClauses.NotFoundException>(async () =>
            await _mediator.Send(new QueryTaskListGet(createdTaskList.Id)));
    }
    
    [Fact]
    public async void CommandTaskListDelete_Throws()
    {
        await Assert.ThrowsAnyAsync<Exception>(async () =>
            await _mediator.Send(new CommandTaskListDelete(_notPossibleTaskListId)));
        await Assert.ThrowsAsync<Ardalis.GuardClauses.NotFoundException>(async () =>
            await _mediator.Send(new CommandTaskListDelete(_notPossibleTaskListId)));
    }
}