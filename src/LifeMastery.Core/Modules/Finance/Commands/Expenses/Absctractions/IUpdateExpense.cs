namespace LifeMastery.Core.Modules.Finance.Commands.Expenses.Absctractions;

public interface IUpdateExpense
{
    Task Execute(UpdateExpenseCommand command, CancellationToken token = default);
}