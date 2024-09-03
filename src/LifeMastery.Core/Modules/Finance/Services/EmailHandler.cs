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

        foreach (var emailSubscription in emailSubs)
        {
            var messages = await emailProvider.GetMessages(emailSubscription.Email, cancellationToken);
            var contents = messages.SelectMany(m => m.AttachmentContents).ToArray();

            foreach (var content in contents)
            {
                var existingExpense = await expenseRepository.GetBySource(content, cancellationToken);
                if (existingExpense != null)
                    continue;

                var parsedExpense = expenseParser.Parse(content);
                if (parsedExpense == null)
                    continue;

                var currency = await currencyRepository.GetByName(parsedExpense.Currency, cancellationToken) 
                    ?? throw new ApplicationException($"Currency '{parsedExpense.Currency}' was not found.");

                var expense = new Expense(parsedExpense.Amount, currency)
                {
                    Source = content,
                    ParsedPlace = parsedExpense.Place,
                    Date = parsedExpense.Date,
                    EmailSubscription = emailSubscription
                };

                if (emailSubscription.Rules.Count != 0)
                {
                    var rule = emailSubscription.Rules.FirstOrDefault(r => r.Place == parsedExpense.Place);
                    if (rule != null)
                    {
                        expense.Category = rule.Category;
                    }
                }

                expenseRepository.Put(expense);
            }
        }

        await unitOfWork.Commit();

        foreach (var emailSub in emailSubs)
        {
            await emailProvider.RemoveMessages(emailSub.Email, cancellationToken);
        }
    }
}
