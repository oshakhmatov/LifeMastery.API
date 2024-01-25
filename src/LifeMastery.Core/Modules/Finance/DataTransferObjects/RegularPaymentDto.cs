using LifeMastery.Core.Modules.Finance.Enums;
using LifeMastery.Core.Modules.Finance.Models;

namespace LifeMastery.Core.Modules.Finance.DataTransferObjects;

public sealed class RegularPaymentDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal? Amount { get; set; }
    public Period Period { get; set; }
    public int? DeadlineDay { get; set; }
    public int? DeadlineMonth { get; set; }
    public bool IsAdvanced { get; set; }

    public static RegularPaymentDto FromModel(RegularPayment regularPayment)
    {
        return new RegularPaymentDto
        {
            Id = regularPayment.Id,
            Name = regularPayment.Name,
            IsAdvanced = regularPayment.IsAdvanced,
            Amount = regularPayment.Amount,
            Period = regularPayment.Period,
            DeadlineDay = regularPayment.DeadlineDay,
            DeadlineMonth = regularPayment.DeadlineMonth
        };
    }
}