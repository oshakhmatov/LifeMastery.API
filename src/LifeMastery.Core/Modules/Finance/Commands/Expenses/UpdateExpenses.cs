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
                if (expense.ParsedPlace == null)
                    continue;

                var rule = emailSub.Rules.FirstOrDefault(r => expense.ParsedPlace.Contains(r.Place, StringComparison.OrdinalIgnoreCase));
                if (rule != null)
                {
                    expense.Category = rule.Category;
                }
            }
        }
    }
}
