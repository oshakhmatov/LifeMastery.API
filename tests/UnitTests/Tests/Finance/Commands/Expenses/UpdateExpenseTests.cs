using AutoFixture;
using LifeMastery.Core.Modules.Finance.Commands.Expenses;
using LifeMastery.Core.Modules.Finance.Models;
using LifeMastery.Core.Modules.Finance.Repositories;
using Moq;

namespace UnitTests.Tests.Finance.Commands.Expenses;

public sealed class UpdateExpenseTests : TestBase
{
    private readonly Mock<IExpenseRepository> expenseRepository;
    private readonly Mock<IExpenseCategoryRepository> expenseCategoryRepository;
    private readonly UpdateExpense sut;

    public UpdateExpenseTests()
    {
        expenseRepository = fixture.Freeze<Mock<IExpenseRepository>>();
        expenseCategoryRepository = fixture.Freeze<Mock<IExpenseCategoryRepository>>();
        sut = fixture.Create<UpdateExpense>();
    }

    [Fact]
    public async Task ExpenseExists_UpdatesExpense()
    {
        // Arrange
        var command = fixture.Create<UpdateExpenseCommand>();

        var expense = fixture.Create<Expense>();
        expenseRepository.Setup(repo => repo.Get(command.ExpenseId, CancellationToken.None)).ReturnsAsync(expense);

        var category = fixture.Create<ExpenseCategory>();
        expenseCategoryRepository.Setup(repo => repo.Get(command.CategoryId!.Value, CancellationToken.None)).ReturnsAsync(category);

        // Act
        await sut.Execute(command);

        // Assert
        Assert.Equal(command.Amount, expense.Amount);
        Assert.Equal(command.Note, expense.Note);
        Assert.Equal(category, expense.Category);
    }

    [Fact]
    public async Task ExpenseNotExists_ThrowsException()
    {
        // Arrange
        var command = fixture.Create<UpdateExpenseCommand>();

        expenseRepository.Setup(repo => repo.Get(command.ExpenseId, CancellationToken.None)).ReturnsAsync((Expense)null);

        // Act & Assert
        await Assert.ThrowsAsync<ApplicationException>(() => sut.Execute(command));
    }
}