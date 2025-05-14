using LifeMastery.Core.Common;
using System.Globalization;

namespace LifeMastery.Infrastructure.Services;

public sealed class RsCultureProvider : IAppCultureProvider
{
    public CultureInfo CurrentCulture { get; } = CultureInfo.GetCultureInfo("sr-RS");
}
