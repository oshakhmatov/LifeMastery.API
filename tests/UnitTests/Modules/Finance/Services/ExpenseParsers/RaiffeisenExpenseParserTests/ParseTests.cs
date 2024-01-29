using LifeMastery.Core.Modules.Finance.DataTransferObjects;
using LifeMastery.Core.Modules.Finance.Services;
using System.Globalization;

namespace UnitTests.Modules.Finance.Services.ExpenseParsers.RaiffeisenExpenseParserTests;

public class ParseTests
{
    [Fact]
    public void AmountWithCommaAndDot_ReturnsCorrectlyParsedExpense()
    {
        // Arrange
        var expected = new ParsedExpenseDto
        {
            Amount = 1430.70m,
            Date = new DateOnly(2024, 1, 18),
            Place = "Mikromarket 110 Novi Sad RS"
        };

        var cut = new RaiffeisenExpenseParser();

        // Act
        var result = cut.Parse("Koriscenje kartice 5356**9996\r\nDatum: 18.01.2024\r\nIznos: 1.430,70 RSD\r\nMesto: Mikromarket 110 Novi Sad RS\r\nStanje: ***");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expected.Amount, result.Amount);
        Assert.Equal(expected.Date, result.Date);
    }

    [Fact]
    public void AmountWithComma_ReturnsCorrectlyParsedExpense()
    {
        // Arrange
        var expected = new ParsedExpenseDto
        {
            Amount = 901.29m,
            Date = new DateOnly(2024, 1, 18),
            Place = "Mikromarket 110 Novi Sad RS"
        };

        var cut = new RaiffeisenExpenseParser();

        // Act
        var result = cut.Parse("Koriscenje kartice 5356**9996\r\nDatum: 18.01.2024\r\nIznos: 901,29 RSD\r\nMesto: Mikromarket 110 Novi Sad RS\r\nStanje: ***");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expected.Amount, result.Amount);
        Assert.Equal(expected.Date, result.Date);
    }
}
