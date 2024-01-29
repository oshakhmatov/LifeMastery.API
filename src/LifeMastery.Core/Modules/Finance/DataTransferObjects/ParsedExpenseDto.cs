namespace LifeMastery.Core.Modules.Finance.DataTransferObjects;

public class ParsedExpenseDto
{
    public decimal Amount { get; set; }
    public string Place { get; set; }
    public DateOnly Date { get; set; }
}
