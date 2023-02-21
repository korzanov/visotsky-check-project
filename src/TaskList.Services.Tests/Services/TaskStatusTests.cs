using TaskList.Contracts.Commands;
using TaskList.Contracts.Queries;
using TaskList.Contracts.Responses;

namespace TaskList.Services.Tests.Services;

public class ServiceTaskStatusTests : ClassFixture
{
    public ServiceTaskStatusTests(ServicesFixture servicesFixture) : base(servicesFixture) {}

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(5)]
    public async void CmdSetDefaults_Success(int count)
    {
        for (var i = 0; i < count; i++)
            await Mediator.Send(new CommandTaskStatusSetDefaults());

        var statuses = (await Mediator.Send(new QueryTaskStatusGetAll())).ToArray();

        Assert.Equal(ResponseTaskStatus.Defaults.Length, statuses.Length);
        foreach(var status in ResponseTaskStatus.Defaults)
            Assert.True(statuses.Contains(status));
    }

    [Fact]
    public async void CmdGetDefault_Success()
    {
        await Mediator.Send(new CommandTaskStatusSetDefaults());
        
        var defaultStatus = await Mediator.Send(new QueryTaskStatusGetDefault());

        Assert.Equal(ResponseTaskStatus.Default, defaultStatus);
    }

    [Fact]
    public async void CmdGetAll_Success()
    {
        await Mediator.Send(new CommandTaskStatusSetDefaults());
        
        var statuses = (await Mediator.Send(new QueryTaskStatusGetAll())).ToArray();

        Assert.True(ResponseTaskStatus.Defaults.Length <= statuses.Length);
        foreach(var status in ResponseTaskStatus.Defaults)
            Assert.True(statuses.Contains(status));
    }
}