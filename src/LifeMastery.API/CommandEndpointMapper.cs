using System.Reflection;
using System.Text.RegularExpressions;
using LifeMastery.Domain.Abstractions;
using Microsoft.AspNetCore.Routing;

namespace LifeMastery.API;

public static partial class CommandEndpointMapper
{
    public static void MapCommands(this IEndpointRouteBuilder endpoints)
    {
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .Where(t => !t.IsAbstract && !t.IsInterface);

        foreach (var type in types)
        {
            var route = ToKebabCase(type.Name);

            var ifaceWithResponse = type.GetInterfaces()
                .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommand<,>));

            if (ifaceWithResponse is not null)
            {
                var requestType = ifaceWithResponse.GetGenericArguments()[0];
                var responseType = ifaceWithResponse.GetGenericArguments()[1];

                typeof(CommandEndpointMapper)
                    .GetMethod(nameof(MapCommandWithResponse), BindingFlags.Static | BindingFlags.NonPublic)!
                    .MakeGenericMethod(type, requestType, responseType)
                    .Invoke(null, [endpoints, route]);

                continue;
            }

            var ifaceWithRequest = type.GetInterfaces()
                .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommand<>));

            if (ifaceWithRequest is not null)
            {
                var requestType = ifaceWithRequest.GetGenericArguments()[0];

                typeof(CommandEndpointMapper)
                    .GetMethod(nameof(MapCommandWithRequest), BindingFlags.Static | BindingFlags.NonPublic)!
                    .MakeGenericMethod(type, requestType)
                    .Invoke(null, [endpoints, route]);

                continue;
            }

            if (typeof(ICommand).IsAssignableFrom(type))
            {
                typeof(CommandEndpointMapper)
                    .GetMethod(nameof(MapCommandNoRequest), BindingFlags.Static | BindingFlags.NonPublic)!
                    .MakeGenericMethod(type)
                    .Invoke(null, [endpoints, route]);
            }
        }
    }

    private static void MapCommandWithRequest<TCommand, TRequest>(
        IEndpointRouteBuilder endpoints,
        string route)
        where TCommand : ICommand<TRequest>
    {
        endpoints.MapPost($"/{route}", async context =>
        {
            var command = context.RequestServices.GetRequiredService<TCommand>();
            var request = await context.Request.ReadFromJsonAsync<TRequest>()
                ?? throw new InvalidOperationException("Invalid request body");
            var ct = context.RequestAborted;

            await command.Execute(request, ct);
            context.Response.StatusCode = StatusCodes.Status204NoContent;
        })
        .WithName(typeof(TCommand).Name);
    }

    private static void MapCommandWithResponse<TCommand, TRequest, TResponse>(
        IEndpointRouteBuilder endpoints,
        string route)
        where TCommand : ICommand<TRequest, TResponse>
    {
        endpoints.MapPost($"/{route}", async context =>
        {
            var command = context.RequestServices.GetRequiredService<TCommand>();
            TRequest request;

            if (typeof(TRequest) == typeof(Unit) || typeof(TRequest).GetProperties().Length == 0)
            {
                request = Activator.CreateInstance<TRequest>();
            }
            else
            {
                request = await context.Request.ReadFromJsonAsync<TRequest>()
                    ?? throw new InvalidOperationException("Invalid request body");
            }

            var ct = context.RequestAborted;
            var result = await command.Execute(request, ct);

            await context.Response.WriteAsJsonAsync(result, cancellationToken: ct);
        })
        .WithName(typeof(TCommand).Name);
    }

    private static void MapCommandNoRequest<TCommand>(
        IEndpointRouteBuilder endpoints,
        string route)
        where TCommand : ICommand
    {
        endpoints.MapPost($"/{route}", async context =>
        {
            var command = context.RequestServices.GetRequiredService<TCommand>();
            var ct = context.RequestAborted;

            await command.Execute(ct);
            context.Response.StatusCode = StatusCodes.Status204NoContent;
        })
        .WithName(typeof(TCommand).Name);
    }

    private static string ToKebabCase(string input)
    {
        return KebabCase().Replace(input, "-$1").ToLowerInvariant();
    }

    [GeneratedRegex("(?<!^)([A-Z])")]
    private static partial Regex KebabCase();
}
