using AutoFixture;
using LifeMastery.Core.Modules.Finance.Commands.Expenses;
using LifeMastery.Core.Modules.Finance.Models;
using LifeMastery.Core.Modules.Finance.Repositories;
using Moq;

namespace UnitTests.Tests.Finance.Commands.Expenses;

public sealed class AddExpenseTests : TestBase
{
    private readonly Mock<IExpenseRepository> expenseRepository;
    private readonly Mock<IExpenseCategoryRepository> expenseCategoryRepository;
    private readonly PutExpense sut;

    public AddExpenseTests()
    {
        expenseRepository = fixture.Freeze<Mock<IExpenseRepository>>();
        expenseCategoryRepository = fixture.Freeze<Mock<IExpenseCategoryRepository>>();
        sut = fixture.Create<PutExpense>();
    }

    [Fact]
    public async Task WithExistingCategory_AddsExpense()
    {
        // Arrange
        var command = fixture.Create<PutExpenseRequest>();

        // Act
        await sut.Execute(command);

        // Assert
        expenseRepository.Verify(repo => repo.Put(It.IsAny<Expense>()), Times.Once);
        expenseRepository.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task WithNonExistingCategory_ThrowsException()
    {
        // Arrange
        var command = fixture.Create<PutExpenseRequest>();

        expenseCategoryRepository.Setup(repo => repo.Get(command.CategoryId!.Value, CancellationToken.None)).ReturnsAsync((ExpenseCategory)null);

        // Act & Assert
        await Assert.ThrowsAsync<ApplicationException>(() => sut.Execute(command));
    }
}