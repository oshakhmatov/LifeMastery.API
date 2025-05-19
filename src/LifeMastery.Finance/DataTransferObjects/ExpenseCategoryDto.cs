namespace LifeMastery.Finance.DataTransferObjects;

public record ExpenseCategoryDto(
    int Id,
    string Name,
    bool IsFood,
    string? Color,
    int? FamilyMemberId);
