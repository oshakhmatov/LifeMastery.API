using LifeMastery.Finance.Models;

namespace LifeMastery.Finance.DataTransferObjects;

public class EmailSubscriptionDto
{
    public required int Id { get; init; }
    public required string Email { get; init; }
    public required bool IsActive { get; init; }
    public required ExpenseCreationRuleDto[] Rules { get; init; }
}