namespace LifeMastery.Finance.DataTransferObjects;

public record EarningDto(
    int Id,
    decimal Amount,
    string FamilyMemberName,
    int PeriodYear,
    int PeriodMonth);
