namespace LifeMastery.Finance.DataTransferObjects;

public record FamilyMemberBudgetDto(
    string FamilyMemberName,
    decimal NetSavings,
    decimal PersonalExpenses,
    decimal SharedContribution);
