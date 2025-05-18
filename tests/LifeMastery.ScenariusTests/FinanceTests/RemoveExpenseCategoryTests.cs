using LifeMastery.Finance.Models;
using LifeMastery.ScenariusTests.TestSupport;
using Scenarius;
using System.Net;

namespace LifeMastery.ScenariusTests.FinanceTests;

public class RemoveExpenseCategoryTests : TestBase
{
    [Fact]
    public Task Should_Remove_Empty_ExpenseCategory()
    {
        return RunScenario(s => s
            .Given(new ExpenseCategory("Empty", isFood: false).With("Id", 10))
            .Post("/remove-expense-category", new { Id = 10 })
            .ExpectStatus(HttpStatusCode.NoContent)
            .ExpectDb<ExpenseCategory>(Should.BeEmpty<ExpenseCategory>())
        );
    }

    [Fact]
    public Task Should_Reject_Removal_If_Category_Has_Expenses()
    {
        return RunScenario(s => s
            .Given(new Expense(20m, new Currency("USD"))
            {
                Category = new ExpenseCategory("Food", isFood: true).With("Id", 10)
            })
            .Post("/remove-expense-category", new { Id = 10 })
            .ExpectStatus(HttpStatusCode.BadRequest)
            .ExpectErrorMessage("Expense category with ID '10' cannot be removed because it contains expenses.")
            .ExpectDb<ExpenseCategory>(Should.Contain<ExpenseCategory>(c => c.Id == 10))
        );
    }

    [Fact]
    public Task Should_Reject_Removal_Of_Nonexistent_Category()
    {
        return RunScenario(s => s
            .Post("/remove-expense-category", new { Id = 999 })
            .ExpectStatus(HttpStatusCode.BadRequest)
            .ExpectErrorMessage("Expense category with ID '999' was not found.")
            .ExpectDb<ExpenseCategory>(Should.BeEmpty<ExpenseCategory>())
        );
    }
}
