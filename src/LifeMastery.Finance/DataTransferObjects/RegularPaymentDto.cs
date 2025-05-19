using LifeMastery.Finance.Enums;

namespace LifeMastery.Finance.DataTransferObjects;

public sealed class RegularPaymentDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal? Amount { get; set; }
    public decimal? ApproximateAmount { get; set; }
    public Period Period { get; set; }
    public int? DeadlineDay { get; set; }
    public int? DeadlineMonth { get; set; }
    public int? PayFromDay { get; set; }
    public bool IsAdvanced { get; set; }
    public bool IsPaid { get; set; }
    public bool IsTax { get; set; }
    public PaymentDto[] Payments { get; set; }
}