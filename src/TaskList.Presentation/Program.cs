using Microsoft.EntityFrameworkCore;
using TaskList.Domain.Repositories;
using TaskList.Persistence;
using TaskList.Persistence.Repositories;
using TaskList.Presentation.Middlewares;
using TaskList.Services;
using TaskList.Services.Abstractions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IServiceManager, ServiceManager>();
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
builder.Services.AddDbContext<RepositoryDbContext>(SetConnectionString);
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

await ApplyMigrations(app.Services);

app.Run();

static async Task ApplyMigrations(IServiceProvider serviceProvider)
{
    using var scope = serviceProvider.CreateScope();
    await using var dbContext = scope.ServiceProvider.GetRequiredService<RepositoryDbContext>();
    await dbContext.Database.EnsureCreatedAsync();
    await dbContext.Database.MigrateAsync();
}

static void SetConnectionString(DbContextOptionsBuilder dbContextOptionsBuilder)
{
#if RELEASE
    var connectionString = Environment.GetEnvironmentVariable("dbConnectionString");
#else
    const string connectionString = "Host=127.0.0.1;Port=5432;Database=task_list_db;Username=postgres_user;Password=postgres_pwd";
#endif
    dbContextOptionsBuilder.UseNpgsql(connectionString);
}