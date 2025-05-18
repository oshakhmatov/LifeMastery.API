using LifeMastery.Domain.Abstractions;
using LifeMastery.Finance.Models;

namespace LifeMastery.Finance.Commands.ExpenseCreationRules;

public sealed class RemoveExpenseCreationRule(
    IRepository<EmailSubscription> subscriptions,
    IUnitOfWork unitOfWork) : ICommand<RemoveExpenseCreationRule.Request>
{
    public async Task Execute(Request request, CancellationToken token)
    {
        var subscription = await subscriptions.GetByIdAsync(request.EmailSubscriptionId, token)
            ?? throw new AppException($"Email subscription with ID '{request.EmailSubscriptionId}' was not found.");

        subscription.RemoveRuleById(request.ExpenseCreationRuleId);

        await unitOfWork.Commit(token);
    }

    public record Request(int ExpenseCreationRuleId, int EmailSubscriptionId);
}
