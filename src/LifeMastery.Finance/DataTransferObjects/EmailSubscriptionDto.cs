using LifeMastery.Finance.Models;

namespace LifeMastery.Finance.DataTransferObjects;

public class EmailSubscriptionDto
{
    public required int Id { get; init; }
    public required string Email { get; init; }
    public required bool IsActive { get; init; }
    public required ExpenseCreationRuleDto[] Rules { get; init; }
}

public static class EmailSubscriptionProjections
{
    public static EmailSubscriptionDto ToDto(this EmailSubscription emailSubscription)
    {
        return new EmailSubscriptionDto
        {
            Id = emailSubscription.Id,
            Email = emailSubscription.Email,
            IsActive = emailSubscription.IsActive,
            Rules = emailSubscription.Rules
                .Select(r => r.ToDto(emailSubscription.Id))
                .OrderBy(r => r.Place)
                .ToArray()
        };
    }
}
