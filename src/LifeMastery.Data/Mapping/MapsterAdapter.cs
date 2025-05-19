using LifeMastery.Domain.Abstractions;
using Mapster;

namespace LifeMastery.Infrastructure.Mapping;

public class MapsterAdapter : IObjectMapper
{
    public TDestination Map<TSource, TDestination>(TSource source)
        => source.Adapt<TDestination>();

    public TDestination Map<TDestination>(object source)
        => source.Adapt<TDestination>();
}
