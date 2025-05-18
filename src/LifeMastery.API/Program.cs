using LifeMastery.API;
using LifeMastery.API.Middleware;
using LifeMastery.Finance;
using LifeMastery.Infrastructure.Services.Abstractions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using var scope = app.Services.CreateScope();
using var migrationService = scope.ServiceProvider.GetRequiredService<IMigrationService>();
await migrationService.Migrate();

app.UseGlobalExceptionHandling();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x =>
{
    x.AllowAnyMethod();
    x.AllowAnyHeader();
    x.WithOrigins("http://localhost:81", "http://localhost:4200");
});
app.UseAuthorization();
app.MapCommands();
app.MapControllers();

var appUrl = app.Environment.IsDevelopment() ? "http://*:82" : "http://*:80";
app.Run(appUrl);

public partial class Program { }