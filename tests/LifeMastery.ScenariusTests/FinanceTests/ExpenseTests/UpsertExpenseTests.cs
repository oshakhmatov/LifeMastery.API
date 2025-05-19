using FluentAssertions;

namespace LifeMastery.ScenariusTests.FinanceTests.ExpenseTests;

public class UpsertExpenseTests : TestBase
{
    [Fact]
    public Task Should_Create_Expense_With_Currency_And_Without_Category()
    {
        return RunScenario(s => s
            .Given(new Currency("USD"), out var currencyId)
            .Post("/upsert-expense", new
            {
                Amount = 12.5m,
                CurrencyId = currencyId,
                Date = new DateTime(2024, 12, 1),
                Note = "Coffee"
            })
            .ExpectStatus(HttpStatusCode.NoContent)
            .ExpectDb<Expense>(items => items.Should().Contain(e =>
                e.Amount == 12.5m &&
                e.Note == "Coffee" &&
                e.CurrencyId == currencyId &&
                e.CategoryId == null))
        );
    }

    [Fact]
    public Task Should_Create_Expense_With_Category_And_Currency()
    {
        return RunScenario(s => s
            .Given(new Currency("USD"), out var currencyId)
            .Given(new ExpenseCategory("Food", isFood: true), out var categoryId)
            .Post("/upsert-expense", new
            {
                Amount = 99,
                CurrencyId = currencyId,
                CategoryId = categoryId,
                Date = new DateTime(2024, 12, 5),
                Note = "Dinner"
            })
            .ExpectStatus(HttpStatusCode.NoContent)
            .ExpectDb<Expense>(items => items.Should().Contain(e =>
                e.Amount == 99 &&
                e.Note == "Dinner" &&
                e.CurrencyId == currencyId &&
                e.CategoryId == categoryId))
        );
    }

    [Fact]
    public Task Should_Reject_Expense_With_Invalid_Currency()
    {
        return RunScenario(s => s
            .Post("/upsert-expense", new
            {
                Amount = 12.5m,
                CurrencyId = 999,
                Date = new DateTime(2024, 12, 1),
                Note = "Invalid currency"
            })
            .ExpectStatus(HttpStatusCode.BadRequest)
            .ExpectErrorMessage("Currency with ID '999' was not found.")
            .ExpectDb(Should.BeEmpty<Expense>())
        );
    }

    [Fact]
    public Task Should_Reject_Expense_With_Invalid_Category()
    {
        return RunScenario(s => s
            .Given(new Currency("USD"), out var currencyId)
            .Post("/upsert-expense", new
            {
                Amount = 25,
                CurrencyId = currencyId,
                CategoryId = 999,
                Date = new DateTime(2024, 12, 1),
                Note = "Invalid category"
            })
            .ExpectStatus(HttpStatusCode.BadRequest)
            .ExpectErrorMessage("Expense category with ID '999' was not found.")
            .ExpectDb(Should.BeEmpty<Expense>())
        );
    }

    [Fact]
    public Task Should_Reject_Update_Of_Nonexistent_Expense()
    {
        return RunScenario(s => s
            .Given(new Currency("USD"), out var currencyId)
            .Post("/upsert-expense", new
            {
                Id = 999,
                Amount = 77,
                CurrencyId = currencyId,
                Date = new DateTime(2024, 10, 10),
                Note = "Trying to update non-existing"
            })
            .ExpectStatus(HttpStatusCode.BadRequest)
            .ExpectErrorMessage("Expense with ID '999' was not found.")
            .ExpectDb(Should.BeEmpty<Expense>())
        );
    }
}
