using LifeMastery.Core.Common;
using LifeMastery.Core.Modules.Finance.Models;
using LifeMastery.Core.Modules.Finance.Repositories;

namespace LifeMastery.Core.Modules.Finance.Commands;

public sealed class PutExpenseCreationRuleRequest
{
    public int EmailSubscriptionId { get; set; }
    public int? Id { get; set; }
    public string Place { get; set; }
    public int CategoryId { get; set; }
}

public sealed class PutExpenseCreationRule : CommandBase<PutExpenseCreationRuleRequest>
{
    private readonly IEmailSubscriptionRepository emailSubscriptionRepository;
    private readonly IExpenseCategoryRepository expenseCategoryRepository;

    public PutExpenseCreationRule(IUnitOfWork unitOfWork, IEmailSubscriptionRepository emailSubscriptionRepository, IExpenseCategoryRepository expenseCategoryRepository) : base(unitOfWork)
    {
        this.emailSubscriptionRepository = emailSubscriptionRepository;
        this.expenseCategoryRepository = expenseCategoryRepository;
    }

    protected override async Task OnExecute(PutExpenseCreationRuleRequest request, CancellationToken token)
    {
        var emailSubscription = await emailSubscriptionRepository.Get(request.EmailSubscriptionId, token)
           ?? throw new Exception($"Email subscription with ID='{request.Id}' was not found.");

        var expenseCategory = await expenseCategoryRepository.Get(request.CategoryId)
            ?? throw new Exception($"Expense category with ID='{request.Id}' was not found.");

        if (request.Id.HasValue)
        {
            var expenseCreationRule = emailSubscription.GetRule(request.Id.Value);

            expenseCreationRule.Place = request.Place;
            expenseCreationRule.Category = expenseCategory;
        }
        else
        {
            var expenseCreationRule = new ExpenseCreationRule(request.Place, expenseCategory);

            emailSubscription.AddRule(expenseCreationRule);
        }
    }
}