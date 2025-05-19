namespace LifeMastery.Finance.DataTransferObjects;

public class ExpenseCategoryDto
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public required bool IsFood { get; init; }
    public required string? Color { get; init; }
    public required int? FamilyMemberId { get; init; }
}