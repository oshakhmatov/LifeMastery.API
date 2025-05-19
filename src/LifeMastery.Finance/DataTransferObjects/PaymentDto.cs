namespace LifeMastery.Finance.DataTransferObjects;

public class PaymentDto
{
    public int Id { get; set; }
    public int RegularPaymentId { get; set; }
    public DateOnly Date { get; set; }
    public decimal Amount { get; set; }
    public int PeriodYear { get; set; }
    public int? PeriodMonth { get; set; }
    public string PeriodName { get; set; }
}