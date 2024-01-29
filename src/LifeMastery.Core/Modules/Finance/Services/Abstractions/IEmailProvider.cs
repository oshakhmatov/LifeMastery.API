using LifeMastery.Core.Modules.Finance.DataTransferObjects;

namespace LifeMastery.Core.Modules.Finance.Services.Abstractions;

public interface IEmailProvider
{
    Task<EmailMessage[]> GetMessages(string sender, CancellationToken cancellationToken);
    Task RemoveMessages(string sender, CancellationToken cancellationToken);
}
