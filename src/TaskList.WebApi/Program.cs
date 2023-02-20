using Microsoft.AspNetCore.Identity;
using TaskList.DbInfrastructure.Data;
using TaskList.DbInfrastructure.Identity;
using TaskList.Domain.Repositories;
using TaskList.WebApi.Security;
using TaskList.WebApi.StartUps;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RepositoryDbContext>(StartUpDataBase.SetConnectionString<RepositoryDbContext>);
builder.Services.AddDbContext<TaskListIdentityDbContext>(StartUpDataBase.SetConnectionString<TaskListIdentityDbContext>);
builder.Services.AddScoped<IRepositoryPersonalInfo, RepositoryPersonalInfo>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped(typeof(IRepositoryReadOnly<>), typeof(EfRepository<>));

builder.Services.AddIdentity<TaskListAppUser, IdentityRole>(StartUpIdentity.UseEasyPassword)
    .AddEntityFrameworkStores<TaskListIdentityDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddAuthentication(StartUpJwtBearer.SetAuthenticationOptions)
    .AddJwtBearer(o => StartUpJwtBearer.SetJwtBearerOptions(o, builder));
builder.Services.AddSingleton<JwtConfig>();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(StartUpSwagger.SetSwaggerOptions);
builder.Services.AddMediatR(TaskList.Services.RegisterHelper.RegisterAssembly);
builder.Services.AddAutoMapper(TaskList.Services.RegisterHelper.GetAssembly());

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

await StartUpDataBase.ApplyIdentityMigrations(app.Services);
await StartUpDataBase.ApplyRepositoryMigrations(app.Services);

app.Run();