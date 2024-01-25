using LifeMastery.Core.Common;
using LifeMastery.Core.Modules.Finance.Repositories;

namespace LifeMastery.Core.Modules.Finance.Commands;

public sealed class RemoveEmailSubscription : CommandBase<int>
{
    private readonly IEmailSubscriptionRepository emailSubscriptionRepository;

    public RemoveEmailSubscription(IUnitOfWork unitOfWork, IEmailSubscriptionRepository emailSubscriptionRepository) : base(unitOfWork)
    {
        this.emailSubscriptionRepository = emailSubscriptionRepository;
    }

    protected override async Task OnExecute(int id, CancellationToken token)
    {
        var emailSubscription = await emailSubscriptionRepository.Get(id, token) ??
            throw new Exception($"Email subscription with ID '{id}' was not found.");

        emailSubscriptionRepository.Remove(emailSubscription);
    }
}
