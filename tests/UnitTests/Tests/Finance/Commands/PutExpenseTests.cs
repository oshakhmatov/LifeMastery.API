using AutoFixture;
using LifeMastery.Core.Modules.Finance.Commands;
using LifeMastery.Core.Modules.Finance.Models;
using LifeMastery.Core.Modules.Finance.Repositories;
using Moq;

namespace UnitTests.Modules.Finance.Commands;

public sealed class PutExpenseTests : TestBase
{
    private readonly Mock<IExpenseRepository> _expenseRepositoryMock;
    private readonly Mock<IExpenseCategoryRepository> _expenseCategoryRepositoryMock;
    private readonly PutExpense _sut;

    public PutExpenseTests()
    {
        _expenseRepositoryMock = fixture.Freeze<Mock<IExpenseRepository>>();
        _expenseCategoryRepositoryMock = fixture.Freeze<Mock<IExpenseCategoryRepository>>();
        _sut = fixture.Create<PutExpense>();
    }

    [Fact]
    public async Task WithIdAndExistingExpense_UpdatesExpense()
    {
        // Arrange
        var request = fixture.Create<PutExpenseRequest>();

        var expense = fixture.Create<Expense>();
        _expenseRepositoryMock.Setup(repo => repo.Get(request.Id!.Value)).ReturnsAsync(expense);

        var category = fixture.Create<ExpenseCategory>();
        _expenseCategoryRepositoryMock.Setup(repo => repo.Get(request.CategoryId!.Value)).ReturnsAsync(category);

        // Act
        await _sut.Execute(request);

        // Assert
        Assert.Equal(request.Amount, expense.Amount);
        Assert.Equal(request.Note, expense.Note);
        Assert.Equal(category, expense.Category);
    }

    [Fact]
    public async Task WithIdAndNonExistingExpense_ThrowsException()
    {
        // Arrange
        var request = fixture.Create<PutExpenseRequest>();

        _expenseRepositoryMock.Setup(repo => repo.Get(request.Id!.Value)).ReturnsAsync((Expense)null);

        // Act & Assert
        await Assert.ThrowsAsync<ApplicationException>(() => _sut.Execute(request));
    }

    [Fact]
    public async Task WithoutId_AddsExpense()
    {
        // Arrange
        var request = fixture.Create<PutExpenseRequest>();
        request.Id = null;

        // Act
        await _sut.Execute(request);

        // Assert
        _expenseRepositoryMock.Verify(repo => repo.Put(It.IsAny<Expense>()), Times.Once);
    }

    [Fact]
    public async Task NonExistingCategory_ThrowsException()
    {
        // Arrange
        var request = fixture.Create<PutExpenseRequest>();

        _expenseCategoryRepositoryMock.Setup(repo => repo.Get(request.CategoryId!.Value)).ReturnsAsync((ExpenseCategory)null);

        // Act & Assert
        await Assert.ThrowsAsync<ApplicationException>(() => _sut.Execute(request));
    }
}