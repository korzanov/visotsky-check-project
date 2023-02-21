using TaskList.Contracts.Commands;
using TaskList.Contracts.Queries;
using TaskList.Contracts.Responses;

namespace TaskList.Services.Tests.Services;

public class TaskStatusRecordTests : ClassFixture
{
    private readonly ResponseTaskStatus _defaultStatus;
    private readonly Guid _existTaskId;
    private readonly Guid _existTaskListId;
    
    public TaskStatusRecordTests(ServicesFixture servicesFixture) : base(servicesFixture)
    {
        Mediator.Send(new CommandTaskStatusSetDefaults()).GetAwaiter();
        _defaultStatus = Mediator.Send(new QueryTaskStatusGetDefault()).Result;
        var taskList = Mediator.Send(new CommandTaskListCreate(AnyString, AnyString)).Result;
        _existTaskListId = taskList.Id;
        var task = Mediator.Send(new CommandTaskCreate(AnyString, AnyString, taskList.Id)).Result;
        _existTaskId = task.Id;
    }

    [Fact]
    public async void CmdTaskStatusRecordCreate_Default_Success()
    {
        var record = await Mediator.Send(new CommandTaskStatusRecordCreate(_existTaskId, _defaultStatus.Id));
        
        Assert.Equal(_defaultStatus.Name, record.StatusName);
    }

    [Fact]
    public async void CmdTaskStatusRecordCreate_All_Success()
    {
        var taskStatuses = await Mediator.Send(new QueryTaskStatusGetAll());

        foreach (var status in taskStatuses)
        {
            var record = await Mediator.Send(new CommandTaskStatusRecordCreate(_existTaskId, status.Id));
            Assert.Equal(status.Name, record.StatusName);
        }
    }

    [Fact]
    public async void CmdTaskStatusRecordCreate_StatusNotFound()
    {
        await Assert.ThrowsAsync<Ardalis.GuardClauses.NotFoundException>( async () =>
            await Mediator.Send(new CommandTaskStatusRecordCreate(_existTaskId, NotPossibleId)));
    }

    [Fact]
    public async void CmdTaskStatusRecordCreate_TaskNotFound()
    {
        await Assert.ThrowsAsync<Ardalis.GuardClauses.NotFoundException>( async () =>
            await Mediator.Send(new CommandTaskStatusRecordCreate(NotPossibleId, _defaultStatus.Id)));
    }
    
    [Fact]
    public async void QueryTaskStatusRecordGetLast_Success()
    {
        var task = await Mediator.Send(new CommandTaskCreate(AnyString, AnyString, _existTaskListId));

        var statuses = (await Mediator.Send(new QueryTaskStatusGetAll())).ToArray();

        Assert.True(statuses.Any(), $"no statuses see {nameof(TaskStatusTests)}");
        foreach (var status in statuses)
        {
            await Mediator.Send(new CommandTaskStatusRecordCreate(task.Id, status.Id));

            var record = await Mediator.Send(new QueryTaskStatusRecordGetLast(task.Id));
            Assert.Equal(status.Name, record.StatusName);
        }
    }
    
    [Fact]
    public async void QueryTaskStatusRecordGetLast_TaskNotFound()
    {
        await Assert.ThrowsAsync<Ardalis.GuardClauses.NotFoundException>( async () =>
            await Mediator.Send(new QueryTaskStatusRecordGetLast(NotPossibleId)));
    }
}