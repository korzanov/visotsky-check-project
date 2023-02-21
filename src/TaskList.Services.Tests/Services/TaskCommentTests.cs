using TaskList.Contracts.Commands;
using TaskList.Contracts.Queries;

namespace TaskList.Services.Tests.Services;

public class TaskCommentTests : ClassFixture
{
    private readonly Guid _existTaskId;
    
    public TaskCommentTests(ServicesFixture servicesFixture) : base(servicesFixture)
    {
        var taskList = Mediator.Send(new CommandTaskListCreate(AnyString, AnyString)).Result;
        var task = Mediator.Send(new CommandTaskCreate(AnyString, AnyString, taskList.Id)).Result;
        _existTaskId = task.Id;
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("some msg")]
    public async void CmdTaskCommentCreate_Success(string message)
    {
        var comment = await Mediator.Send(new CommandTaskCommentCreate(_existTaskId, message));
        Assert.Equal(message, comment.Message);
    }
    
    [Fact]
    public async void CmdTaskCommentCreate_TaskNotFound()
    {
        await Assert.ThrowsAsync<Ardalis.GuardClauses.NotFoundException>(async () =>
            await Mediator.Send(new CommandTaskCommentCreate(NotPossibleId, AnyString)));
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("some msg")]
    public async void CmdTaskCommentUpdate_Success(string message)
    {
        var comment = await Mediator.Send(new CommandTaskCommentCreate(_existTaskId, AnyString));

        var updatedComment = await Mediator.Send(new CommandTaskCommentUpdate(comment.Id, message));
        
        Assert.Equal(message, updatedComment.Message);
    }
    
    [Fact]
    public async void CmdTaskCommentUpdate_NotFound()
    {
        await Assert.ThrowsAsync<Ardalis.GuardClauses.NotFoundException>(async () =>
            await Mediator.Send(new CommandTaskCommentUpdate(NotPossibleId, AnyString)));
    }
    
    [Fact]
    public async void CmdTaskCommentDelete_Success()
    {
        var createdComment = await Mediator.Send(new CommandTaskCommentCreate(_existTaskId, AnyString));

        await Mediator.Send(new CommandTaskCommentDelete(createdComment.Id));

        var taskComments = await Mediator.Send(new QueryTaskCommentGetAll(_existTaskId));
        
        Assert.False(taskComments.Any(comment => comment.Id == createdComment.Id));
    }
    
    [Fact]
    public async void CmdTaskCommentDelete_NotFound()
    {
        await Assert.ThrowsAsync<Ardalis.GuardClauses.NotFoundException>(async () =>
            await Mediator.Send(new CommandTaskCommentDelete(NotPossibleId)));
    }
    
    [Fact]
    public async void CmdTaskCommentGetAll_Success()
    {
        var createdComment = await Mediator.Send(new CommandTaskCommentCreate(_existTaskId, AnyString));

        var taskComments = await Mediator.Send(new QueryTaskCommentGetAll(_existTaskId));
        
        Assert.True(taskComments.Any(comment => comment.Id == createdComment.Id));
    }
    
    [Fact]
    public async void CmdTaskCommentGetAll_NotFound()
    {
        await Assert.ThrowsAsync<Ardalis.GuardClauses.NotFoundException>(async () =>
            await Mediator.Send(new QueryTaskCommentGetAll(NotPossibleId)));
    }
}