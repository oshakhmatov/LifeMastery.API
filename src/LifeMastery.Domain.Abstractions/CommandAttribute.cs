namespace LifeMastery.Domain.Abstractions;

[AttributeUsage(AttributeTargets.Class)]
public class CommandAttribute(string name) : Attribute
{
    public string Name { get; } = name;
}
