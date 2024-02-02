using System.Globalization;

namespace LifeMastery.Core.Common;

public static class DateHelper
{
    public static string GetMonthName(int month)
    {
        return new CultureInfo("ru-RU").DateTimeFormat.GetMonthName(month);
    }
}
