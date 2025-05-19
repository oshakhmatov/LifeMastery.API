using LifeMastery.API;
using LifeMastery.API.Middleware;
using LifeMastery.Infrastructure.Services.Abstractions;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);
var supportedCultures = new[]
{
    new CultureInfo("ru-RU"),
    new CultureInfo("en-US")
};
var defaultCulture = supportedCultures[0];
CultureInfo.DefaultThreadCurrentCulture = defaultCulture;
CultureInfo.DefaultThreadCurrentUICulture = defaultCulture;
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("ru-RU"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
};
localizationOptions.RequestCultureProviders.Clear();

builder.Services.AddApplication(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "LifeMastery.API", Version = "v1" });
    c.CustomSchemaIds(type =>
        type.IsNested
            ? $"{type.DeclaringType!.Name}_{type.Name}"
            : type.Name);
    c.DocumentFilter<AutoCommandDocumentFilter>();
});

builder.Services.AddAuthorization();
builder.Services.AddCors();

var app = builder.Build();

using var scope = app.Services.CreateScope();
using var migrationService = scope.ServiceProvider.GetRequiredService<IMigrationService>();
await migrationService.Migrate();

app.UseRequestLocalization(localizationOptions);
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

var appUrl = app.Environment.IsDevelopment() ? "http://*:82" : "http://*:80";
app.Run(appUrl);

public partial class Program { }