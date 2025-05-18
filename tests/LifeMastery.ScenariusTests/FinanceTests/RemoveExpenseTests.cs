using LifeMastery.Finance.Models;
using Scenarius;
using System.Net;
using LifeMastery.ScenariusTests.TestSupport;

namespace LifeMastery.ScenariusTests.FinanceTests;

public class RemoveExpenseTests : TestBase
{
    [Fact]
    public Task Should_Remove_Existing_Expense()
    {
        return RunScenario(s => s
            .Given(new Expense(45, new Currency("USD").With("Id", 1))
                .With("Id", 10))
            .Post("/remove-expense", new { Id = 10 })
            .ExpectStatus(HttpStatusCode.NoContent)
            .ExpectDb<Expense>(Should.BeEmpty<Expense>())
        );
    }

    [Fact]
    public Task Should_Reject_Removal_Of_Nonexistent_Expense()
    {
        return RunScenario(s => s
            .Post("/remove-expense", new { Id = 999 })
            .ExpectStatus(HttpStatusCode.BadRequest)
            .ExpectErrorMessage("Expense with ID '999' was not found.")
            .ExpectDb<Expense>(Should.BeEmpty<Expense>())
        );
    }
}
