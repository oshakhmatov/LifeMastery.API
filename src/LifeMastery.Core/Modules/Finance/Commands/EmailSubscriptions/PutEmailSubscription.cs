using LifeMastery.Core.Common;
using LifeMastery.Core.Modules.Finance.Models;
using LifeMastery.Core.Modules.Finance.Repositories;

namespace LifeMastery.Core.Modules.Finance.Commands.EmailSubscriptions;

public sealed class PutEmailSubscriptionRequest
{
    public int? Id { get; set; }
    public required string Email { get; set; }
    public required bool IsActive { get; set; }
}

public sealed class PutEmailSubscription(
    IUnitOfWork unitOfWork,
    IEmailSubscriptionRepository emailSubscriptionRepository) : CommandBase<PutEmailSubscriptionRequest>(unitOfWork)
{
    protected override async Task OnExecute(PutEmailSubscriptionRequest request, CancellationToken token)
    {
        if (request.Id.HasValue)
        {
            var emailSubscription = await emailSubscriptionRepository.Get(request.Id.Value, token) ??
                throw new Exception($"Email subscription with ID='{request.Id}' was not found.");

            emailSubscription.Email = request.Email;
            emailSubscription.IsActive = request.IsActive;
        }
        else
        {
            emailSubscriptionRepository.Put(new EmailSubscription(request.Email, request.IsActive));
        }
    }
}