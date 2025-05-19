namespace LifeMastery.ScenariusTests.FinanceTests.ExpenseCategories;

public class RemoveExpenseCategoryTests : TestBase
{
    [Fact]
    public Task Should_Remove_Empty_ExpenseCategory()
    {
        return RunScenario(s => s
            .Given(new ExpenseCategory("Empty", isFood: false), out var categoryId)
            .Post("/remove-expense-category", new { Id = categoryId })
            .ExpectStatus(HttpStatusCode.NoContent)
            .ExpectDb(Should.BeEmpty<ExpenseCategory>())
        );
    }

    [Fact]
    public Task Should_Reject_Removal_If_Category_Has_Expenses()
    {
        return RunScenario(s => s
            .Given(new ExpenseCategory("Food", isFood: true), out var categoryId)
            .Given(new Expense(20m, new Currency("USD")) { CategoryId = categoryId }, out _)
            .Post("/remove-expense-category", new { Id = categoryId })
            .ExpectStatus(HttpStatusCode.BadRequest)
            .ExpectErrorMessage($"Expense category with ID '{categoryId}' cannot be removed because it contains expenses.")
            .ExpectDb(Should.Contain<ExpenseCategory>(c => c.Id == categoryId))
        );
    }

    [Fact]
    public Task Should_Reject_Removal_Of_Nonexistent_Category()
    {
        return RunScenario(s => s
            .Post("/remove-expense-category", new { Id = 999 })
            .ExpectStatus(HttpStatusCode.BadRequest)
            .ExpectErrorMessage("Expense category with ID '999' was not found.")
            .ExpectDb(Should.BeEmpty<ExpenseCategory>())
        );
    }
}
