using LifeMastery.Finance.Models;

namespace LifeMastery.Finance.DataTransferObjects;

public record EmailSubscriptionDto(
    int Id,
    string Email,
    bool IsActive,
    ExpenseCreationRuleDto[] Rules);
