using AccountManager.Application.Services;
using AccountManager.Domain.Interfaces;
using AccountManager.Infrastructure.Data;
using AccountManager.Infrastructure.Repositories;
using AccountManager.Shared.JWT;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IUserRepository, AccountRepository>();
builder.Services.AddScoped<ISessionRepository, SessionRepository>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<AccountService>();
builder.Services.AddSingleton<JwtTokenGenerator>();

builder.Services.Configure<SessionOptions>(builder.Configuration.GetSection("Session"));


var configuration = builder.Configuration;

builder.Services.AddDbContext<AccountDbContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("AccountManager")));


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AccountDbContext>();
    dbContext.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthorization();

app.MapControllers();

app.Run();
