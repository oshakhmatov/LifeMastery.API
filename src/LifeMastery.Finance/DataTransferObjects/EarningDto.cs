namespace LifeMastery.Finance.DataTransferObjects;

public sealed class EarningDto
{
    public required int Id { get; init; }
    public required decimal Amount { get; init; }
    public required string FamilyMemberName { get; init; }
    public required int PeriodYear { get; init; }
    public required int PeriodMonth { get; init; }
}