using AutoFixture;
using LifeMastery.Core.Modules.Finance.Commands;
using LifeMastery.Core.Modules.Finance.Models;
using LifeMastery.Core.Modules.Finance.Repositories;
using Moq;
using UnitTests.Common;

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
    public async Task Execute_WithExistingId_ShouldUpdateExpense()
    {
        // Arrange
        var request = fixture.Create<PutExpenseRequest>();

        var existingExpense = fixture.Create<Expense>();
        _expenseRepositoryMock.Setup(repo => repo.Get(request.Id!.Value)).ReturnsAsync(existingExpense);

        // Act
        await _sut.Execute(request, CancellationToken.None);

        // Assert
        _expenseRepositoryMock.Verify(repo => repo.Put(It.IsAny<Expense>()), Times.Once);
    }

    [Fact]
    public async Task Execute_WithNewId_ShouldAddExpense()
    {
        // Arrange
        var request = fixture.Create<PutExpenseRequest>();
        request.Id = null;

        // Act
        await _sut.Execute(request, CancellationToken.None);

        // Assert
        _expenseRepositoryMock.Verify(repo => repo.Put(It.IsAny<Expense>()), Times.Once);
    }

    [Fact]
    public async Task OnExecute_WithCategoryId_ShouldContainCategoryInExpense()
    {
        // Arrange
        var request = fixture.Create<PutExpenseRequest>();
        request.CategoryId = fixture.Create<int>();

        var expense = fixture.Create<Expense>();
        _expenseRepositoryMock.Setup(repo => repo.Get(request.Id!.Value)).ReturnsAsync(expense);

        var category = fixture.Create<ExpenseCategory>();
        _expenseCategoryRepositoryMock.Setup(repo => repo.Get(request.CategoryId.Value)).ReturnsAsync(category);

        // Act
        await _sut.Execute(request, CancellationToken.None);

        // Assert
        _expenseRepositoryMock.Verify(repo => repo.Put(It.IsAny<Expense>()), Times.Once);

        Assert.NotNull(expense.Category);
        Assert.Equal(category, expense.Category);
    }

    [Fact]
    public async Task UpdateExistingExpense_WithNonExistingExpense_ShouldThrowException()
    {
        // Arrange
        var request = fixture.Create<PutExpenseRequest>();
        _expenseRepositoryMock.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync((Expense)null);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _sut.Execute(request, CancellationToken.None));
    }

    [Fact]
    public async Task GetCategory_WithNonExistingCategoryId_ShouldThrowException()
    {
        // Arrange
        var request = fixture.Create<PutExpenseRequest>();

        var nonExistingCategoryId = fixture.Create<int>();
        _expenseCategoryRepositoryMock.Setup(repo => repo.Get(It.IsAny<int>())).ReturnsAsync((ExpenseCategory)null);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _sut.Execute(request, CancellationToken.None));
    }
}