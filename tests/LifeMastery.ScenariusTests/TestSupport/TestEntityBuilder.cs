using System.Reflection;

namespace LifeMastery.ScenariusTests.TestSupport;

public static class TestEntityBuilder
{
    public static T With<T>(this T entity, string propertyName, object? value)
    {
        typeof(T)
            .GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            ?.SetValue(entity, value);

        return entity;
    }
}