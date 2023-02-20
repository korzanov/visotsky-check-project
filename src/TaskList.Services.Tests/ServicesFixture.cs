using MediatR;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskList.DbInfrastructure.Data;
using TaskList.Domain.Repositories;

namespace TaskList.Services.Tests;

public class ServicesFixture
{
    public ServicesFixture()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddDbContext<RepositoryDbContext>(
            optionsBuilder => optionsBuilder.UseInMemoryDatabase(databaseName: "TestsDb"));
        serviceCollection.AddAutoMapper(RegisterHelper.GetAssembly());
        serviceCollection.AddMediatR(RegisterHelper.RegisterAssembly);
        serviceCollection.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        serviceCollection.AddScoped(typeof(IRepositoryReadOnly<>), typeof(EfRepository<>));
        _serviceProvider = serviceCollection.BuildServiceProvider();
        _serviceProvider.GetRequiredService<RepositoryDbContext>();
    }

    private readonly ServiceProvider _serviceProvider;
    public IMediator Mediator => _serviceProvider.GetRequiredService<IMediator>();
}