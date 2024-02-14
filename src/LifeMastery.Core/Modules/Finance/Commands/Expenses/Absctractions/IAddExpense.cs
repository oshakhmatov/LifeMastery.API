namespace LifeMastery.Core.Modules.Finance.Commands.Expenses.Absctractions;

public interface IAddExpense
{
    Task Execute(AddExpenseCommand command, CancellationToken token = default);
}