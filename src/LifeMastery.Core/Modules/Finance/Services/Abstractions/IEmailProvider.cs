namespace LifeMastery.Core.Modules.Finance.Services.Abstractions;

public interface IEmailProvider
{
    Task<string?[]> GetMessages(string sender, CancellationToken cancellationToken);
    Task RemoveMessages(string sender, CancellationToken cancellationToken);
}
