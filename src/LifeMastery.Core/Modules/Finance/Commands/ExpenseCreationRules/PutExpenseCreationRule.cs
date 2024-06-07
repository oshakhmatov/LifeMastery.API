using LifeMastery.Core.Common;
using LifeMastery.Core.Modules.Finance.Models;
using LifeMastery.Core.Modules.Finance.Repositories;

namespace LifeMastery.Core.Modules.Finance.Commands.ExpenseCreationRules;

public sealed class PutExpenseCreationRuleRequest
{
    public int? Id { get; set; }
    public required int EmailSubscriptionId { get; set; }
    public required int CategoryId { get; set; }
    public required string Place { get; set; }
}

public sealed class PutExpenseCreationRule(
    IUnitOfWork unitOfWork,
    IEmailSubscriptionRepository emailSubscriptionRepository,
    IExpenseCategoryRepository expenseCategoryRepository) : CommandBase<PutExpenseCreationRuleRequest>(unitOfWork)
{
    protected override async Task OnExecute(PutExpenseCreationRuleRequest request, CancellationToken token)
    {
        var emailSubscription = await emailSubscriptionRepository.Get(request.EmailSubscriptionId, token)
           ?? throw new Exception($"Email subscription with ID='{request.Id}' was not found.");

        var expenseCategory = await expenseCategoryRepository.Get(request.CategoryId, token)
            ?? throw new Exception($"Expense category with ID='{request.Id}' was not found.");

        if (request.Id.HasValue)
        {
            var expenseCreationRule = emailSubscription.GetRule(request.Id.Value);

            expenseCreationRule.Place = request.Place;
            expenseCreationRule.Category = expenseCategory;
        }
        else
        {
            emailSubscription.AddRule(new ExpenseCreationRule(request.Place, expenseCategory));
        }
    }
}