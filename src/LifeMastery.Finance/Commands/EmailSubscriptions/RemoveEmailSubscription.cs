using LifeMastery.Domain.Abstractions;
using LifeMastery.Finance.Models;

namespace LifeMastery.Finance.Commands.EmailSubscriptions;

public sealed class RemoveEmailSubscription(IRepository<EmailSubscription> subscriptions, IUnitOfWork unitOfWork) : ICommand<RemoveEmailSubscription.Request>
{
    public async Task Execute(Request request, CancellationToken token)
    {
        var subscription = await subscriptions.GetByIdAsync(request.Id, token)
            ?? throw new AppException($"Email subscription with ID '{request.Id}' was not found.");

        subscriptions.Remove(subscription);
        await unitOfWork.Commit(token);
    }

    public record Request(int Id);
}
