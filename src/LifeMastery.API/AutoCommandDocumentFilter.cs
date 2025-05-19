using LifeMastery.Domain.Abstractions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.RegularExpressions;

namespace LifeMastery.API;

public class AutoCommandDocumentFilter : IDocumentFilter
{
    private static readonly Regex KebabCaseRegex = new("(?<!^)([A-Z])", RegexOptions.Compiled);

    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var commandTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(x =>
            {
                try { return x.GetTypes(); } catch { return Array.Empty<Type>(); }
            })
            .Where(t => !t.IsAbstract && !t.IsInterface)
            .ToList();

        foreach (var type in commandTypes)
        {
            if (type.GetInterfaces().FirstOrDefault(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommand<,>)) is { } ifaceWithResponse)
            {
                var requestType = ifaceWithResponse.GetGenericArguments()[0];
                var responseType = ifaceWithResponse.GetGenericArguments()[1];
                AddCommandPath(swaggerDoc, context, type, requestType, responseType);
            }
            else if (type.GetInterfaces().FirstOrDefault(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommand<>)) is { } ifaceWithRequest)
            {
                var requestType = ifaceWithRequest.GetGenericArguments()[0];
                AddCommandPath(swaggerDoc, context, type, requestType, typeof(void));
            }
            else if (typeof(ICommand).IsAssignableFrom(type))
            {
                AddCommandPath(swaggerDoc, context, type, null, typeof(void));
            }
        }
    }

    private static void AddCommandPath(OpenApiDocument doc, DocumentFilterContext ctx, Type commandType, Type? requestType, Type responseType)
    {
        var route = KebabCaseRegex.Replace(commandType.Name, "-$1").ToLowerInvariant();

        var operation = new OpenApiOperation
        {
            Summary = $"Executes {commandType.Name}",
            Responses = new OpenApiResponses
            {
                ["200"] = new OpenApiResponse
                {
                    Description = "Success",
                    Content = responseType != typeof(void)
                        ? new Dictionary<string, OpenApiMediaType>
                        {
                            ["application/json"] = new()
                            {
                                Schema = ctx.SchemaGenerator.GenerateSchema(responseType, ctx.SchemaRepository)
                            }
                        }
                        : new Dictionary<string, OpenApiMediaType>()
                }
            },
            RequestBody = requestType is not null && requestType != typeof(void) && requestType != typeof(Unit)
                ? new OpenApiRequestBody
                {
                    Required = true,
                    Content = new Dictionary<string, OpenApiMediaType>
                    {
                        ["application/json"] = new()
                        {
                            Schema = ctx.SchemaGenerator.GenerateSchema(requestType, ctx.SchemaRepository)
                        }
                    }
                }
                : null
        };

        doc.Paths.Add($"/{route}", new OpenApiPathItem
        {
            Operations = { [OperationType.Post] = operation }
        });
    }
}