using LifeMastery.Finance.Enums;

namespace LifeMastery.Finance.DataTransferObjects;

public record RegularPaymentDto(
    int Id,
    string Name,
    decimal? Amount,
    decimal? ApproximateAmount,
    Period Period,
    int? DeadlineDay,
    int? DeadlineMonth,
    int? PayFromDay,
    bool IsAdvanced,
    bool IsPaid,
    bool IsTax,
    bool IsActive,
    PaymentDto[] Payments);
