using LifeMastery.API.Middlewares;
using LifeMastery.Application;
using LifeMastery.Infrastructure.Services.Abstractions;
using Microsoft.Extensions.Options;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

var supportedCultures = new[] { new CultureInfo("ru-RU") };

// Configure RequestLocalizationOptions
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("ru-RU");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;

    foreach (var culture in supportedCultures)
    {
        culture.DateTimeFormat.ShortDatePattern = "dd.MM.yyyy";
    }
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

var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(options.Value);

app.UseCors(x =>
{
    x.AllowAnyMethod();
    x.AllowAnyHeader();
    x.WithOrigins("http://localhost", "http://localhost:4200");
});

app.UseRequestLogging();
app.MapControllers();

app.Run();

public partial class Program { }