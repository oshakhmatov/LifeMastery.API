namespace LifeMastery.Core.Modules.Finance.DataTransferObjects;

public class DailyExpensesDto
{
    public string Date { get; set; }
    public ExpenseDto[] Expenses { get; set; }
}
