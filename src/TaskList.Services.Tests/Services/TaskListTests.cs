using TaskList.Contracts.Commands;
using TaskList.Contracts.Queries;

namespace TaskList.Services.Tests.Services;

public class TaskListTests : ClassFixture
{
    public TaskListTests(ServicesFixture servicesFixture) : base(servicesFixture) {}

    [Theory]
    [InlineData("00000", "000000")]
    [InlineData("11111", "111111")]
    public async void CommandTaskListCreate_Success(string name, string description)
    {
        var responseTaskList = await Mediator.Send(new CommandTaskListCreate(name, description));
        
        Assert.Equal(name, responseTaskList.Name);
        Assert.Equal(description, responseTaskList.Description);
    }
    
    [Theory]
    [InlineData("00000", "000000")]
    [InlineData("11111", "111111")]
    public async void CommandTaskListUpdate_Success(string name, string description)
    {
        var createdTaskList = await Mediator.Send(new CommandTaskListCreate(AnyString, AnyString));
        
        var responseTaskList = await Mediator.Send(new CommandTaskListUpdate(createdTaskList.Id, name, description));
        
        Assert.Equal(name, responseTaskList.Name);
        Assert.Equal(description, responseTaskList.Description);
    }

    [Fact]
    public async void CommandTaskListUpdate_Throws()
    {
        await Assert.ThrowsAnyAsync<Exception>(async () =>
            await Mediator.Send(new CommandTaskListUpdate(NotPossibleId, AnyString, AnyString)));
        await Assert.ThrowsAsync<Ardalis.GuardClauses.NotFoundException>(async () =>
            await Mediator.Send(new CommandTaskListUpdate(NotPossibleId, AnyString, AnyString)));
    }

    [Fact]
    public async void CommandTaskListDelete_Success()
    {
        var createdTaskList = await Mediator.Send(new CommandTaskListCreate(AnyString, AnyString));
        await Mediator.Send(new CommandTaskListDelete(createdTaskList.Id));

        await Assert.ThrowsAsync<Ardalis.GuardClauses.NotFoundException>(async () =>
            await Mediator.Send(new QueryTaskListGet(createdTaskList.Id)));
    }
    
    [Fact]
    public async void CommandTaskListDelete_Throws()
    {
        await Assert.ThrowsAnyAsync<Exception>(async () =>
            await Mediator.Send(new CommandTaskListDelete(NotPossibleId)));
        await Assert.ThrowsAsync<Ardalis.GuardClauses.NotFoundException>(async () =>
            await Mediator.Send(new CommandTaskListDelete(NotPossibleId)));
    }
}