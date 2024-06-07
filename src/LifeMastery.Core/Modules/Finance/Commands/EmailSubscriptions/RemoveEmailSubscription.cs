using LifeMastery.Core.Common;
using LifeMastery.Core.Modules.Finance.Repositories;

namespace LifeMastery.Core.Modules.Finance.Commands.EmailSubscriptions;

public sealed class RemoveEmailSubscription(
    IUnitOfWork unitOfWork,
    IEmailSubscriptionRepository emailSubscriptionRepository) : CommandBase<int>(unitOfWork)
{
    protected override async Task OnExecute(int id, CancellationToken token)
    {
        var emailSubscription = await emailSubscriptionRepository.Get(id, token) ??
            throw new Exception($"Email subscription with ID '{id}' was not found.");

        emailSubscriptionRepository.Remove(emailSubscription);
    }
}
