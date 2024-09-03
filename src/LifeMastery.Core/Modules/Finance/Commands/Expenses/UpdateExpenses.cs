using LifeMastery.Core.Common;
using LifeMastery.Core.Modules.Finance.Repositories;
using LifeMastery.Core.Modules.Finance.Services.Abstractions;

namespace LifeMastery.Core.Modules.Finance.Commands.Expenses;

public class UpdateExpenses(
    IUnitOfWork unitOfWork,
    IEmailSubscriptionRepository emailSubscriptionRepository,
    ICurrencyRepository currencyRepository,
    IExpenseParser expenseParser) : CommandBase(unitOfWork)
{
    protected override async Task OnExecute(CancellationToken token)
    {
        var emailSubs = await emailSubscriptionRepository.ListActiveWithExpenses(token);
        if (emailSubs.Length == 0)
            return;

        foreach (var emailSub in emailSubs)
        {
            if (emailSub.Rules.Count == 0)
                continue;

            foreach (var expense in emailSub.Expenses)
            {
                if (expense.Source == null)
                    continue;

                var parsedExpense = expenseParser.Parse(expense.Source);
                if (parsedExpense == null)
                    continue;

                var currency = await currencyRepository.GetByName(parsedExpense.Currency, token)
                    ?? throw new ApplicationException($"Currency '{parsedExpense.Currency}' was not found.");

                expense.ParsedPlace = parsedExpense.Place;
                expense.Currency = currency;

                var rule = emailSub.Rules.FirstOrDefault(r => parsedExpense.Place.Contains(r.Place, StringComparison.OrdinalIgnoreCase));
                if (rule != null)
                {
                    expense.Category = rule.Category;
                }
            }
        }
    }
}
