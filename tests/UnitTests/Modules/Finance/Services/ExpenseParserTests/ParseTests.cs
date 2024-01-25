using LifeMastery.Core.Modules.Finance.Models;
using LifeMastery.Core.Modules.Finance.Services;
using LifeMastery.Core.Modules.Finance.Services.Abstractions;
using Moq;

namespace UnitTests.Modules.Finance.Services.ExpenseParserTests;

public class ParseTests
{
    [Fact]
    public async Task WeightIsOkAndCouldBeDecreased_ShouldReturnLightAdvise()
    {
        // Arrange
        var productsCategory = new ExpenseCategory("Продукты");

        var expenseCategoryResolver = new Mock<IExpenseCategoryResolver>();
        expenseCategoryResolver.Setup(s => s.ResolveFromPlace("Mikromarket 110 Novi Sad RS")).ReturnsAsync(productsCategory);

        var expected = new Expense(430.70m)
        {
            Date = new DateOnly(2024, 1, 18)
        };

        var cut = new ExpenseParser(expenseCategoryResolver.Object);

        // Act
        var result = await cut.Parse("Koriscenje kartice 5356**9996\r\nDatum: 18.01.2024\r\nIznos: 430,70 RSD\r\nMesto: Mikromarket 110 Novi Sad RS\r\nStanje: ***");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expected.Amount, result.Amount);
        Assert.Equal(expected.Date, result.Date);
    }
}
