using Microsoft.AspNetCore.Identity;
using TaskList.Domain.Interfaces;
using TaskList.DbInfrastructure.Data;
using TaskList.DbInfrastructure.Identity;
using TaskList.WebApi.StartUp;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RepositoryDbContext>(DataBaseStartUp.SetConnectionString<RepositoryDbContext>);
builder.Services.AddDbContext<TaskListIdentityDbContext>(DataBaseStartUp.SetConnectionString<TaskListIdentityDbContext>);
builder.Services.AddScoped<IRepositoryPersonalInfo, RepositoryPersonalInfo>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped(typeof(IRepositoryReadOnly<>), typeof(EfRepository<>));

builder.Services.AddIdentity<TaskListAppUser, IdentityRole>(IdentityStartUp.UseEasyPassword)
    .AddEntityFrameworkStores<TaskListIdentityDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddAuthentication(JwtBearerStartUp.SetAuthenticationOptions)
    .AddJwtBearer(o => JwtBearerStartUp.SetJwtBearerOptions(o, builder));

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(SwaggerStartUp.SetSwaggerOptions);
builder.Services.AddMediatR(TaskList.Services.RegisterHelper.RegisterAssembly);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

await DataBaseStartUp.ApplyIdentityMigrations(app.Services);
await DataBaseStartUp.ApplyRepositoryMigrations(app.Services);

app.Run();