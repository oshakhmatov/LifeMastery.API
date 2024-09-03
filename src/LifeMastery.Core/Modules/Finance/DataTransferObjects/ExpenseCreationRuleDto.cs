using LifeMastery.Core.Modules.Finance.Models;

namespace LifeMastery.Core.Modules.Finance.DataTransferObjects;

public class ExpenseCreationRuleDto
{
    public required int Id { get; init; }
    public required string Place { get; init; }
    public required int CategoryId { get; init; }
    public required string CategoryName { get; init; }
    public required int EmailSubscriptionId { get; init; }
}

public static class ExpenseCreationRuleProjections
{
    public static ExpenseCreationRuleDto ToDto(this ExpenseCreationRule model, int emailSubscriptionId)
    {
        return new ExpenseCreationRuleDto
        {
            Id = model.Id,
            Place = model.Place,
            CategoryId = model.Category.Id,
            CategoryName = model.Category.Name,
            EmailSubscriptionId = emailSubscriptionId
        };
    }
}
