namespace LifeMastery.ScenariusTests.FinanceTests.ExpenseTests;

public class RemoveExpenseTests : TestBase
{
    [Fact]
    public Task Should_Remove_Existing_Expense()
    {
        return RunScenario(s => s
            .Given(new Expense(45, new Currency("USD")), out var expenseId)
            .Post("/remove-expense", new { Id = expenseId })
            .ExpectStatus(HttpStatusCode.NoContent)
            .ExpectDb(Should.BeEmpty<Expense>())
        );
    }

    [Fact]
    public Task Should_Reject_Removal_Of_Nonexistent_Expense()
    {
        return RunScenario(s => s
            .Post("/remove-expense", new { Id = 999 })
            .ExpectStatus(HttpStatusCode.BadRequest)
            .ExpectErrorMessage("Expense with ID '999' was not found.")
            .ExpectDb(Should.BeEmpty<Expense>())
        );
    }
}
