using System.Globalization;

namespace LifeMastery.Domain.Abstractions;

public interface IAppCultureProvider
{
    CultureInfo CurrentCulture { get; }
}

