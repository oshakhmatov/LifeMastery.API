using LifeMastery.Core.Modules.Finance.DataTransferObjects;
using LifeMastery.Core.Modules.Finance.Services.Abstractions;
using System.Globalization;
using System.Text.RegularExpressions;

namespace LifeMastery.Core.Modules.Finance.Services;

public sealed partial class RaiffeisenExpenseParser : IExpenseParser
{
    public ParsedExpenseDto? Parse(string content)
    {
        var place = PlaceRegex().Match(content).Groups[1].Value;
        if (place == "RAIFFEISEN BANK NOVI SAD RS")
            return null;

        var amount = Decimal.Parse(AmountRegex().Match(content).Groups[1].Value.Replace(".", ""), new CultureInfo("ru-RU"));
        var date = DateOnly.ParseExact(DateRegex().Match(content).Groups[1].Value, "dd.MM.yyyy");

        return new ParsedExpenseDto()
        {
            Date = date,
            Place = place,
            Amount = amount
        };
    }

    [GeneratedRegex("Datum:\\s+(\\d{2}\\.\\d{2}\\.\\d{4})")]
    private static partial Regex DateRegex();
    [GeneratedRegex("Iznos:\\s+([\\d.,]+)")]
    private static partial Regex AmountRegex();
    [GeneratedRegex("Mesto:\\s+(.+)")]
    private static partial Regex PlaceRegex();

    
}
