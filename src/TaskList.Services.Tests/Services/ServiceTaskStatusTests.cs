using MediatR;
using TaskList.Contracts.Commands;
using TaskList.Contracts.Queries;
using TaskList.Contracts.Responses;

namespace TaskList.Services.Tests.Services;

public class ServiceTaskStatusTests : IClassFixture<ServicesFixture>
{
    private readonly IMediator _mediator;

    public ServiceTaskStatusTests(ServicesFixture servicesFixture)
    {
        _mediator = servicesFixture.Mediator;
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(5)]
    public async void CmdSetDefaults_Success(int count)
    {
        for (var i = 0; i < count; i++)
            await _mediator.Send(new CommandTaskStatusSetDefaults());

        var statuses = (await _mediator.Send(new QueryTaskStatusGetAll())).ToArray();

        Assert.Equal(ResponseTaskStatus.Defaults.Length, statuses.Length);
        foreach(var status in ResponseTaskStatus.Defaults)
            Assert.True(statuses.Contains(status));
    }

    [Fact]
    public async void CmdGetDefault_Success()
    {
        await _mediator.Send(new CommandTaskStatusSetDefaults());
        
        var defaultStatus = await _mediator.Send(new QueryTaskStatusGetDefault());

        Assert.Equal(ResponseTaskStatus.Default, defaultStatus);
    }

    [Fact]
    public async void CmdGetAll_Success()
    {
        await _mediator.Send(new CommandTaskStatusSetDefaults());
        
        var statuses = (await _mediator.Send(new QueryTaskStatusGetAll())).ToArray();

        Assert.True(ResponseTaskStatus.Defaults.Length <= statuses.Length);
        foreach(var status in ResponseTaskStatus.Defaults)
            Assert.True(statuses.Contains(status));
    }
}