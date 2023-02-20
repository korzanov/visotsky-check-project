using MediatR;

namespace TaskList.WebApi.Tests.Controllers;

public abstract class ControllerWithMediatorTests
{
    protected readonly Mock<IMediator> MediatorMock= new();
    protected IMediator Mediator => MediatorMock.Object;

    protected void SetupMediatrMockAnyRequest<TRequest>() where TRequest : IRequest
    {
        MediatorMock
            .Setup(m => m.Send(It.IsAny<TRequest>(), It.IsAny<CancellationToken>()))
            .Returns(Unit.Task);
    }
    
    protected void SetupMediatrMockAnyRequest<TRequest, TResponse>(TResponse response) where TRequest : IRequest<TResponse>
    {
        MediatorMock
            .Setup(m => m.Send(It.IsAny<TRequest>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(response));
    }
    
    protected void SetupMediatrMockInRequests<TRequest, TResponse>(TResponse response, params TRequest[] requests) where TRequest : IRequest<TResponse>
    {
        MediatorMock
            .Setup(m => m.Send(It.IsIn(requests), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(response));
    }
    
    protected void SetupMediatrMockNotInRequests<TRequest, TResponse>(TResponse response, params TRequest[] requests) where TRequest : IRequest<TResponse>
    {
        MediatorMock
            .Setup(m => m.Send(It.IsNotIn(requests), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(response));
    }
}