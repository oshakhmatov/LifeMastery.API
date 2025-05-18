using LifeMastery.Domain.Abstractions;
using LifeMastery.Finance.Models;

namespace LifeMastery.Finance.Commands.EmailSubscriptions;

public sealed class UpsertEmailSubscription(IRepository<EmailSubscription> subscriptions, IUnitOfWork unitOfWork) : ICommand<UpsertEmailSubscription.Request>
{
    public async Task Execute(Request request, CancellationToken token)
    {
        if (request.Id is not null)
        {
            var subscription = await subscriptions.GetByIdAsync(request.Id.Value, token)
                ?? throw new AppException($"Email subscription with ID '{request.Id}' was not found.");

            subscription.Email = request.Email;
            subscription.IsActive = request.IsActive;
        }
        else
        {
            subscriptions.Add(new EmailSubscription(request.Email, request.IsActive));
        }

        await unitOfWork.Commit(token);
    }

    public record Request(int? Id, string Email, bool IsActive);
}
