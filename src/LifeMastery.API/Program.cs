using LifeMastery.Application;
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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(x =>
{
    x.AllowAnyMethod();
    x.AllowAnyHeader();
    x.WithOrigins("http://localhost", "http://localhost:4200");
});

app.MapControllers();

app.Run();

public partial class Program { }