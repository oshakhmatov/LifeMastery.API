using LifeMastery.Core.Modules.Finance.Models;

namespace LifeMastery.Core.Modules.Finance.DataTransferObjects;

public class EmailSubscriptionDto
{
    public int Id { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; }
    public ExpenseCreationRuleDto[] Rules { get; set; }

    public static EmailSubscriptionDto FromModel(EmailSubscription emailSubscription)
    {
        return new EmailSubscriptionDto
        {
            Id = emailSubscription.Id,
            Email = emailSubscription.Email,
            IsActive = emailSubscription.IsActive,
            Rules = emailSubscription.Rules.Select(r => ExpenseCreationRuleDto.FromModel(r, emailSubscription.Id)).ToArray()
        };
    }
}
