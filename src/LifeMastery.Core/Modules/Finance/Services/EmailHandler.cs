using LifeMastery.Core.Modules.Finance.Models;
using LifeMastery.Core.Modules.Finance.Repositories;
using LifeMastery.Core.Modules.Finance.Services.Abstractions;

namespace LifeMastery.Core.Modules.Finance.Services;

public class EmailHandler
{
    private readonly IEmailProvider emailProvider;
    private readonly IExpenseRepository expenseRepository;
    private readonly IExpenseParser expenseParser;
    private readonly IUnitOfWork unitOfWork;
    private readonly IEmailSubscriptionRepository emailSubscriptionRepository;

    public EmailHandler(
        IEmailProvider emailProvider,
        IExpenseParser expenseParser,
        IExpenseRepository expenseRepository,
        IUnitOfWork unitOfWork,
        IEmailSubscriptionRepository emailSubscriptionRepository)
    {
        this.emailProvider = emailProvider;
        this.expenseParser = expenseParser;
        this.expenseRepository = expenseRepository;
        this.unitOfWork = unitOfWork;
        this.emailSubscriptionRepository = emailSubscriptionRepository;
    }

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
                var existingExpense = await expenseRepository.GetBySource(content);
                if (existingExpense != null)
                    continue;

                var parsedExpense = expenseParser.Parse(content);
                if (parsedExpense == null)
                    continue;

                var expense = new Expense(parsedExpense.Amount)
                {
                    Source = content,
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
