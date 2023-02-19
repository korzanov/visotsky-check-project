using Microsoft.Extensions.DependencyInjection;

namespace TaskList.Services;

public abstract class RegisterHelper
{
    private RegisterHelper() { }

    public static void MediatrConfigure(MediatRServiceConfiguration configuration) 
        => configuration.RegisterServicesFromAssemblyContaining<RegisterHelper>();
}