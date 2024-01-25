using LifeMastery.Core.Modules.Finance.Models;
using LifeMastery.Core.Modules.Finance.Services.Abstractions;
using System.Text.RegularExpressions;

namespace LifeMastery.Core.Modules.Finance.Services;

public sealed partial class ExpenseParser
{
    private readonly IExpenseCategoryResolver expenseCategoryResolver;

    public ExpenseParser(IExpenseCategoryResolver expenseCategoryResolver)
    {
        this.expenseCategoryResolver = expenseCategoryResolver;
    }

    public async Task<Expense> Parse(string content)
    {
        var amount = Decimal.Parse(AmountRegex().Match(content).Groups[1].Value);
        var date = DateOnly.Parse(DateRegex().Match(content).Groups[1].Value);
        var place = PlaceRegex().Match(content).Groups[1].Value;

        var category = await expenseCategoryResolver.ResolveFromPlace(place);

        return new Expense(amount)
        {
            Date = date,
            Source = content,
            Category = category
        };
    }

    [GeneratedRegex("Datum:\\s+(\\d{2}\\.\\d{2}\\.\\d{4})")]
    private static partial Regex DateRegex();
    [GeneratedRegex("Iznos:\\s+([\\d,]+(?:\\.\\d+)?)")]
    private static partial Regex AmountRegex();
    [GeneratedRegex("Mesto:\\s+(.+)")]
    private static partial Regex PlaceRegex();
}
