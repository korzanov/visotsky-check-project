using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace TaskList.Services;

public abstract class RegisterHelper
{
    private RegisterHelper() { }

    public static void RegisterAssembly(MediatRServiceConfiguration configuration) 
        => configuration.RegisterServicesFromAssemblyContaining<RegisterHelper>();
    public static Assembly GetAssembly() 
        => typeof(RegisterHelper).Assembly;
}