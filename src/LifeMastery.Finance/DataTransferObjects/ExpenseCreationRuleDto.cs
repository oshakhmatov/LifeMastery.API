namespace LifeMastery.Finance.DataTransferObjects;

public class ExpenseCreationRuleDto
{
    public required int Id { get; init; }
    public required string Place { get; init; }
    public required int CategoryId { get; init; }
    public required string CategoryName { get; init; }
    public required int EmailSubscriptionId { get; init; }
}