namespace LifeMastery.Finance.DataTransferObjects;

public record PaymentDto(
    int Id,
    int RegularPaymentId,
    DateOnly Date,
    decimal Amount,
    int PeriodYear,
    int? PeriodMonth,
    string PeriodName);
