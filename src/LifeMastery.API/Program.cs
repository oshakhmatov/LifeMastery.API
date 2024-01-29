using LifeMastery.Application;
using LifeMastery.Infrastructure.Services.Abstractions;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

var ruCultureName = "ru-RU";
CultureInfo.CurrentCulture = new CultureInfo(ruCultureName);
CultureInfo.CurrentUICulture = new CultureInfo(ruCultureName);
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture(ruCultureName);
});

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