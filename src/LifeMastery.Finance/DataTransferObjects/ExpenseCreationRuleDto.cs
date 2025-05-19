namespace LifeMastery.Finance.DataTransferObjects;

public record ExpenseCreationRuleDto(
    int Id,
    string Place,
    int CategoryId,
    string CategoryName,
    int EmailSubscriptionId);
