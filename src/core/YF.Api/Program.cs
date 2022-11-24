using YF.Api.Configurations;
using YF.Application;
using YF.Infrastructure;
using YF.Infrastructure.Persistence.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddConfiguration();
builder.Services.AddSettings(builder.Configuration);
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    using(var scope = app.Services.CreateScope())
    {
        var initalizer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializator>();
        await initalizer.SeedAsync();
    }

    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
