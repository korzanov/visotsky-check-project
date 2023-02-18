using Microsoft.Extensions.Logging;
using TaskList.Services.Abstractions;

namespace TaskList.Presentation.Tests.Controllers;

public abstract class BaseControllerTests<T>
{
    private readonly Mock<ILogger<T>> _loggerMock;
    protected readonly Mock<IServiceManager> ServiceManagerMock;

    protected ILogger<T> Logger => _loggerMock.Object;
    protected IServiceManager ServiceManager => ServiceManagerMock.Object;
    
    protected BaseControllerTests()
    {
        _loggerMock = new Mock<ILogger<T>>();
        ServiceManagerMock = new Mock<IServiceManager>();
    }
}