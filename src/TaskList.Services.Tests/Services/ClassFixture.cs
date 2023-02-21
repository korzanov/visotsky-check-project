using MediatR;

namespace TaskList.Services.Tests.Services;

public abstract class ClassFixture  : IClassFixture<ServicesFixture>
{
    protected readonly IMediator Mediator;
    protected readonly Guid NotPossibleId;
    protected readonly string AnyString;
    
    protected ClassFixture(ServicesFixture servicesFixture)
    {
        Mediator = servicesFixture.Mediator;
        NotPossibleId = new Guid("F88C636F-EA8D-424A-887C-C5683410A6B7");
        AnyString = "AD0AB380-AD46-4BC2-962B-16B152E6100E";
    }
}