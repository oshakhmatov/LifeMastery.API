namespace LifeMastery.Core.Modules.Finance.DataTransferObjects;

public class FamilyMemberBudgetDto
{
    public required string FamilyMemberName { get; init; }

    /// <summary>Личные сбережения (доход минус траты)</summary>
    public required decimal NetSavings { get; init; }

    /// <summary>Личные траты (на себя)</summary>
    public required decimal PersonalExpenses { get; init; }

    /// <summary>Вклад в общие расходы</summary>
    public required decimal SharedContribution { get; init; }
}
