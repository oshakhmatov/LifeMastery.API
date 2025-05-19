namespace LifeMastery.Domain.Abstractions;

public interface IObjectMapper
{
    TDestination Map<TSource, TDestination>(TSource source);
    TDestination Map<TDestination>(object source);
}
