using LifeMastery.Domain.Abstractions;
using LifeMastery.Finance.Models;

namespace LifeMastery.Finance.Commands.Expenses;

public sealed class UpdateExpenses(
    IRepository<EmailSubscription> subscriptions,
    IUnitOfWork unitOfWork) : ICommand
{
    public async Task Execute(CancellationToken token)
    {
        var emailSubs = await subscriptions.ListAsync(
            s => s.IsActive && s.Expenses.Count > 0, token);

        foreach (var emailSub in emailSubs)
        {
            if (emailSub.Rules.Count == 0)
                continue;

            foreach (var expense in emailSub.Expenses)
            {
                if (expense.ParsedPlace == null)
                    continue;

                var rule = emailSub.Rules.FirstOrDefault(r =>
                    expense.ParsedPlace.Contains(r.Place, StringComparison.OrdinalIgnoreCase));

                if (rule is not null)
                    expense.Category = rule.Category;
            }
        }

        await unitOfWork.Commit(token);
    }
}
