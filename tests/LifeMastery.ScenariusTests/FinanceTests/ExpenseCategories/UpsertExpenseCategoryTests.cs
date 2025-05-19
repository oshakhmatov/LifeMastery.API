namespace LifeMastery.ScenariusTests.FinanceTests.ExpenseCategories;

public class UpsertExpenseCategoryTests : TestBase
{
    [Fact]
    public Task Should_Create_New_ExpenseCategory()
    {
        return RunScenario(s => s
            .Post("/upsert-expense-category", new { Name = "Transport", IsFood = false, Color = "#123456" })
            .ExpectStatus(HttpStatusCode.NoContent)
            .ExpectDb(Should.Contain<ExpenseCategory>(c => c.Name == "Transport" && c.IsFood == false && c.Color == "#123456"))
        );
    }

    [Fact]
    public Task Should_Update_Existing_ExpenseCategory()
    {
        return RunScenario(s => s
            .Given(new ExpenseCategory("OldName", isFood: true), out var categoryId)
            .Post("/upsert-expense-category", new { Id = categoryId, Name = "UpdatedName", IsFood = false, Color = "#654321" })
            .ExpectStatus(HttpStatusCode.NoContent)
            .ExpectDb(Should.Contain<ExpenseCategory>(c => c.Id == categoryId && c.Name == "UpdatedName" && c.IsFood == false && c.Color == "#654321"))
        );
    }

    [Fact]
    public Task Should_Reject_Duplicate_Category_Name()
    {
        return RunScenario(s => s
            .Given(new ExpenseCategory("Unique", isFood: false), out _)
            .Post("/upsert-expense-category", new { Name = "Unique", IsFood = true, Color = "#abcdef" })
            .ExpectStatus(HttpStatusCode.BadRequest)
            .ExpectErrorMessage("Expense category with name 'Unique' already exists.")
            .ExpectDb(Should.Contain<ExpenseCategory>(c => c.Name == "Unique"))
        );
    }

    [Fact]
    public Task Should_Reject_Update_Of_Nonexistent_Category()
    {
        return RunScenario(s => s
            .Post("/upsert-expense-category", new { Id = 999, Name = "DoesNotExist", IsFood = false, Color = "#ffffff" })
            .ExpectStatus(HttpStatusCode.BadRequest)
            .ExpectErrorMessage("Expense category with ID '999' was not found.")
            .ExpectDb(Should.BeEmpty<ExpenseCategory>())
        );
    }
}
