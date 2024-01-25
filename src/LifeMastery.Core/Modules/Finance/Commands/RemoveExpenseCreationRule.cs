using LifeMastery.Core.Common;
using LifeMastery.Core.Modules.Finance.Repositories;

namespace LifeMastery.Core.Modules.Finance.Commands;

public sealed class RemoveExpenseCreationRuleRequest
{
    public int ExpenseCreationRuleId {  get; set; }
    public int EmailSubscriptionId { get; set; }
}

public sealed class RemoveExpenseCreationRule : CommandBase<RemoveExpenseCreationRuleRequest>
{
    private readonly IEmailSubscriptionRepository emailSubscriptionRepository;

    public RemoveExpenseCreationRule(IUnitOfWork unitOfWork, IEmailSubscriptionRepository emailSubscriptionRepository) : base(unitOfWork)
    {
        this.emailSubscriptionRepository = emailSubscriptionRepository;
    }

    protected override async Task OnExecute(RemoveExpenseCreationRuleRequest request, CancellationToken token)
    {
        var emailSubscription = await emailSubscriptionRepository.Get(request.EmailSubscriptionId, token)
            ?? throw new Exception($"Email subscription rule with ID='{request.EmailSubscriptionId}' was not found.");

        emailSubscription.RemoveRuleById(request.ExpenseCreationRuleId);
    }
}
