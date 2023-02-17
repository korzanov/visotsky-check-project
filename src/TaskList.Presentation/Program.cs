using TaskList.Domain.Repositories;
using TaskList.Persistence;
using TaskList.Persistence.Repositories;
using TaskList.Presentation.StartUp;
using TaskList.Presentation.Middlewares;
using TaskList.Services;
using TaskList.Services.Abstractions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(SwaggerStartUp.SetSwaggerOptions);
builder.Services.AddScoped<IServiceManager, ServiceManager>();
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
builder.Services.AddDbContext<RepositoryDbContext>(DataBaseStartUp.SetConnectionString);
builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.AddAuthentication(JwtBearerStartUp.SetAuthenticationOptions)
    .AddJwtBearer(o => JwtBearerStartUp.SetJwtBearerOptions(o, builder));
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

await DataBaseStartUp.ApplyMigrations(app.Services);

app.Run();