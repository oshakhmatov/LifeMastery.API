using AutoFixture;
using LifeMastery.Application.Modules.Finance.RequestHandlers;
using LifeMastery.Core.Modules.Finance.Commands.Expenses;
using LifeMastery.Core.Modules.Finance.Commands.Expenses.Absctractions;
using Moq;

namespace UnitTests.Tests.Finance.RequestHandlers;

public sealed class PutExpenseTests : TestBase
{
    private readonly Mock<IAddExpense> addExpense;
    private readonly Mock<IUpdateExpense> updateExpense;
    private readonly PutExpense sut;

    public PutExpenseTests()
    {
        addExpense = fixture.Freeze<Mock<IAddExpense>>();
        updateExpense = fixture.Freeze<Mock<IUpdateExpense>>();
        sut = fixture.Create<PutExpense>();
    }

    [Fact]
    public async Task IdIsNotNull_ExecutesUpdateExpenseCommand()
    {
        // Arrange
        var request = fixture.Create<PutExpenseRequest>();

        // Act
        await sut.Execute(request);

        // Assert
        updateExpense.Verify(c => c.Execute(It.IsAny<UpdateExpenseCommand>(), CancellationToken.None), Times.Once());
        addExpense.VerifyNoOtherCalls();
    }

    [Fact]
    public async Task IdIsNull_ExecutesAddExpenseCommand()
    {
        // Arrange
        var request = fixture.Create<PutExpenseRequest>();
        request.Id = null;

        // Act
        await sut.Execute(request);

        // Assert
        addExpense.Verify(c => c.Execute(It.IsAny<AddExpenseCommand>(), CancellationToken.None), Times.Once());
        updateExpense.VerifyNoOtherCalls();
    }
}