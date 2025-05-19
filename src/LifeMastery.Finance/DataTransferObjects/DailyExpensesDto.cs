namespace LifeMastery.Finance.DataTransferObjects;

public record DailyExpensesDto(
    string Date,
    ExpenseDto[] Expenses);
