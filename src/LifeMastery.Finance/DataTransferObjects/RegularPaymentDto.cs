using LifeMastery.Common;
using LifeMastery.Finance.Enums;
using LifeMastery.Finance.Models;

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

public static class RegularPaymentProjection
{
    public static RegularPaymentDto ToDto(this RegularPayment regularPayment)
    {
        return new RegularPaymentDto
        {
            Id = regularPayment.Id,
            Name = regularPayment.Name,
            IsAdvanced = regularPayment.IsAdvanced,
            Amount = regularPayment.Amount,
            ApproximateAmount = GetApproximateAmount(regularPayment),
            Period = regularPayment.Period,
            IsPaid = regularPayment.IsPaid(),
            IsTax = regularPayment.IsTax,
            DeadlineDay = regularPayment.DeadlineDay,
            DeadlineMonth = regularPayment.DeadlineMonth,
            PayFromDay = regularPayment.PayFromDay,
            Payments = regularPayment.Payments.Select(p => PaymentDto.FromModel(p, regularPayment.Id)).ToArray()
        };
    }

    private static decimal? GetApproximateAmount(RegularPayment regularPayment)
    {
        if (regularPayment.Amount != null || regularPayment.Payments.Count == 0)
        {
            return 0;
        }

        return MathHelper.Round(regularPayment.Payments.Average(p => p.Amount));
    }
}