using System.Globalization;

namespace LifeMastery.Common;

public static class DateHelper
{
    public static string GetMonthName(int month)
    {
        return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
    }
}
