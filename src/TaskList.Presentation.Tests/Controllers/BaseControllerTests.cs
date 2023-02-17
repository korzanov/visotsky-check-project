using Microsoft.Extensions.Logging;
using TaskList.Services.Abstractions;

namespace TaskList.Presentation.Tests.Controllers;

public abstract class BaseControllerTests<T>
{
    protected readonly Mock<ILogger<T>> LoggerMock;
    protected readonly Mock<IServiceManager> ServiceManagerMock;
    
    protected BaseControllerTests()
    {
        LoggerMock = new Mock<ILogger<T>>();
        ServiceManagerMock = new Mock<IServiceManager>();
    }
}