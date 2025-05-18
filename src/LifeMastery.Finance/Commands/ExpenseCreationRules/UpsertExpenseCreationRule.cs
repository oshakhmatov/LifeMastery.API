using LifeMastery.Domain.Abstractions;
using LifeMastery.Finance.Models;

namespace LifeMastery.Finance.Commands.ExpenseCreationRules;

public sealed class UpsertExpenseCreationRule(
    IRepository<EmailSubscription> subscriptions,
    IRepository<ExpenseCategory> categories,
    IUnitOfWork unitOfWork) : ICommand<UpsertExpenseCreationRule.Request>
{
    public async Task Execute(Request request, CancellationToken token)
    {
        var subscription = await subscriptions.GetByIdAsync(request.EmailSubscriptionId, token)
            ?? throw new AppException($"Email subscription with ID '{request.EmailSubscriptionId}' was not found.");

        var category = await categories.GetByIdAsync(request.CategoryId, token)
            ?? throw new AppException($"Expense category with ID '{request.CategoryId}' was not found.");

        if (request.Id is not null)
        {
            var rule = subscription.GetRule(request.Id.Value);
            rule.Place = request.Place;
            rule.Category = category;
        }
        else
        {
            subscription.AddRule(new ExpenseCreationRule(request.Place, category));
        }

        await unitOfWork.Commit(token);
    }

    public record Request(
        int? Id,
        int EmailSubscriptionId,
        int CategoryId,
        string Place);
}
