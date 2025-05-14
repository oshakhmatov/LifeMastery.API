using System.Globalization;

namespace LifeMastery.Core.Common;

public interface IAppCultureProvider
{
    CultureInfo CurrentCulture { get; }
}

