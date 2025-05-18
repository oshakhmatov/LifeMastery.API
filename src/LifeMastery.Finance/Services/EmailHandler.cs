using LifeMastery.Domain.Abstractions;
using LifeMastery.Finance.Models;
using LifeMastery.Finance.Services.Abstractions;

namespace LifeMastery.Finance.Services;

public class EmailHandler(
    IEmailProvider emailProvider,
    IExpenseParser expenseParser,
    IRepository<Expense> expenses,
    IUnitOfWork unitOfWork,
    IRepository<EmailSubscription> emailSubscriptions,
    IRepository<Currency> currencies)
{
    public async Task HandleInbox(CancellationToken cancellationToken)
    {
        var emailSubs = await emailSubscriptions.ListAsync(es => es.IsActive, cancellationToken);
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
                    var existingExpense = await expenses.FirstOrDefaultAsync(e => e.TransactionId == parsedExpense.TransactionId, cancellationToken);
                    if (existingExpense != null)
                    {
                        continue;
                    }

                    var currency = await currencies.FirstOrDefaultAsync(c => c.Name == parsedExpense.Currency, cancellationToken)
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

                    expenses.Add(expense);
                }
            }
        }

        foreach (var emailSub in emailSubs)
        {
            await emailProvider.RemoveMessages(emailSub.Email, cancellationToken);
        }
    }
}
