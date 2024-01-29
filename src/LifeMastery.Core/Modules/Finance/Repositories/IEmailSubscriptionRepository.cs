using LifeMastery.Core.Modules.Finance.Models;

namespace LifeMastery.Core.Modules.Finance.Repositories;

public interface IEmailSubscriptionRepository
{
    public Task<EmailSubscription?> Get(int id, CancellationToken token);
    public void Add(EmailSubscription emailSubscription);
    public void Update(EmailSubscription emailSubscription);
    public void Remove(EmailSubscription emailSubscription);
    public Task<EmailSubscription[]> List(CancellationToken token);
    public Task<EmailSubscription[]> ListActive(CancellationToken token);
    public Task<EmailSubscription[]> ListActiveWithExpenses(CancellationToken token);
}
