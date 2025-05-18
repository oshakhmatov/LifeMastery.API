using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Scenarius;
using System;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace Scenarius;

public class ScenarioBuilder(HttpClient client, DbContext db, ScenarioOptions? options = null)
{
    private readonly List<Func<Task>> _given = [];
    private readonly List<Func<Task>> _actions = [];
    private readonly List<Func<Task>> _asserts = [];
    private HttpResponseMessage? _lastResponse;
    private readonly ScenarioOptions _options = options ?? new ScenarioOptions();

    public static ScenarioBuilder New(HttpClient client, DbContext db, ScenarioOptions? options = null) => new(client, db, options);

    public ScenarioBuilder Given<T>(T entity) where T : class
    {
        _given.Add(async () =>
        {
            db.Set<T>().Add(entity);
            await db.SaveChangesAsync();
        });
        return this;
    }

    public ScenarioBuilder Post(string url, object body)
    {
        _actions.Add(async () =>
        {
            _lastResponse = await client.PostAsJsonAsync(url, body);
        });
        return this;
    }

    public ScenarioBuilder ExpectStatus(HttpStatusCode code)
    {
        _asserts.Add(() =>
        {
            _lastResponse!.StatusCode.Should().Be(code);
            return Task.CompletedTask;
        });
        return this;
    }

    public ScenarioBuilder ExpectErrorMessage(string expectedMessage)
    {
        _asserts.Add(async () =>
        {
            var body = await _lastResponse!.Content.ReadAsStringAsync();

            if (!string.IsNullOrWhiteSpace(_options.ErrorMessagePath))
            {
                using var doc = JsonDocument.Parse(body);
                var current = doc.RootElement;

                foreach (var part in _options.ErrorMessagePath.Split('.'))
                {
                    if (current.ValueKind != JsonValueKind.Object || !current.TryGetProperty(part, out var next))
                    {
                        throw new InvalidOperationException($"Could not find path '{_options.ErrorMessagePath}' in error body.");
                    }
                    current = next;
                }

                current.GetString().Should().Be(expectedMessage);
            }
            else
            {
                body.Should().Contain(expectedMessage);
            }
        });
        return this;
    }

    public ScenarioBuilder ExpectDb<T>(Action<IEnumerable<T>> assert) where T : class
    {
        _asserts.Add(async () =>
        {
            var data = await db.Set<T>().ToListAsync();
            assert(data);
        });
        return this;
    }

    public async Task ExecuteAsync()
    {
        foreach (var g in _given) await g();
        foreach (var act in _actions) await act();
        foreach (var check in _asserts) await check();
    }
}

public class ScenarioOptions
{
    public string? ErrorMessagePath { get; set; }
}

public static class Should
{
    public static Action<IEnumerable<T>> BeEmpty<T>() => items => items.Should().BeEmpty();
    public static Action<IEnumerable<T>> Contain<T>(Expression<Func<T, bool>> predicate) => items => items.Should().Contain(predicate);
}
