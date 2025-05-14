namespace LifeMastery.Core.Modules.Finance.DataTransferObjects;

public class FamilyMemberBudgetDto
{
    public required string FamilyMemberName { get; init; }
    public required decimal Savings { get; init; }
    public required decimal PrivateExpenses { get; init; }
    public required decimal SharedExpenses { get; init; }
}
