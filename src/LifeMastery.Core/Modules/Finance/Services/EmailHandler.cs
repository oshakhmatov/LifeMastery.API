using LifeMastery.Core.Modules.Finance.Models;
using LifeMastery.Core.Modules.Finance.Repositories;
using LifeMastery.Core.Modules.Finance.Services.Abstractions;

namespace LifeMastery.Core.Modules.Finance.Services;

public class EmailHandler(
    IEmailProvider emailProvider,
    IExpenseParser expenseParser,
    IExpenseRepository expenseRepository,
    IUnitOfWork unitOfWork,
    IEmailSubscriptionRepository emailSubscriptionRepository,
    ICurrencyRepository currencyRepository)
{
    public async Task HandleInbox(CancellationToken cancellationToken)
    {
        var emailSubs = await emailSubscriptionRepository.ListActive(cancellationToken);
        if (emailSubs.Length == 0)
            return;

        foreach (var emailSub in emailSubs)
        {
            var messages = await emailProvider.GetMessages(emailSub.Email, cancellationToken);

            foreach (var content in messages)
            {
                if (content == null)
                {
                    continue;
                }

                var parsedExpenses = expenseParser.Parse(content);
                if (parsedExpenses.Length == 0)
                {
                    continue;
                }

                foreach (var parsedExpense in parsedExpenses)
                {
                    var existingExpense = await expenseRepository.GetByTransactionId(parsedExpense.TransactionId, cancellationToken);
                    if (existingExpense != null)
                    {
                        continue;
                    }

                    var currency = await currencyRepository.GetByName(parsedExpense.Currency, cancellationToken)
                        ?? throw new ApplicationException($"Currency '{parsedExpense.Currency}' was not found.");

                    var expense = new Expense(parsedExpense.Amount, currency)
                    {
                        Source = parsedExpense.Source,
                        TransactionId = parsedExpense.TransactionId,
                        ParsedPlace = parsedExpense.Place,
                        Date = parsedExpense.Date,
                        EmailSubscription = emailSub
                    };

                    if (emailSub.Rules.Count != 0)
                    {
                        var rule = emailSub.Rules.FirstOrDefault(r => expense.ParsedPlace.Contains(r.Place, StringComparison.OrdinalIgnoreCase));
                        if (rule != null)
                        {
                            expense.Category = rule.Category;
                        }
                    }

                    expenseRepository.Put(expense);
                }
            }
        }

        await unitOfWork.Commit();

        foreach (var emailSub in emailSubs)
        {
            await emailProvider.RemoveMessages(emailSub.Email, cancellationToken);
        }
    }
}
