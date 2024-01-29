using LifeMastery.Core.Modules.Finance.Models;

namespace LifeMastery.Core.Modules.Finance.DataTransferObjects;

public class ExpenseCreationRuleDto
{
    public int Id { get; set; }
    public string Place { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public int EmailSubscriptionId { get; set; }

    public static ExpenseCreationRuleDto FromModel(ExpenseCreationRule model, int emailSubscriptionId)
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
