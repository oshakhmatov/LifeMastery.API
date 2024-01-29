using LifeMastery.Core.Modules.Finance.Models;
using LifeMastery.Core.Modules.Finance.Repositories;
using LifeMastery.Core.Modules.Finance.Services.Abstractions;

namespace LifeMastery.Core.Modules.Finance.Commands;

public class UpdateExpenses
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IEmailSubscriptionRepository emailSubscriptionRepository;
    private readonly IExpenseParser expenseParser;

    public UpdateExpenses(IUnitOfWork unitOfWork, IEmailSubscriptionRepository emailSubscriptionRepository, IExpenseParser expenseParser)
    {
        this.unitOfWork = unitOfWork;
        this.emailSubscriptionRepository = emailSubscriptionRepository;
        this.expenseParser = expenseParser;
    }

    public async Task Execute(CancellationToken cancellationToken)
    {
        var emailSubs = await emailSubscriptionRepository.ListActiveWithExpenses(cancellationToken);
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

                var rule = emailSub.Rules.FirstOrDefault(r => r.Place == parsedExpense.Place);
                if (rule != null)
                {
                    expense.Category = rule.Category;
                }
            }
        }

        await unitOfWork.Commit();
    }
}
