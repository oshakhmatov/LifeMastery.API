﻿using LifeMastery.Core.Modules.Finance.DataTransferObjects;
using LifeMastery.Core.Modules.Finance.Services;

namespace UnitTests.Tests.Finance.Services.ExpenseParsers;

public class RaiffeisenExpenseParserTests
{
    [Fact]
    public void AmountWithCommaAndDot_ReturnsCorrectlyParsedAmount()
    {
        // Arrange
        var expected = new ParsedExpenseDto
        {
            Amount = 1430.70m,
            Date = new DateOnly(2024, 1, 18),
            Place = "Mikromarket 110 Novi Sad RS",
            Currency = "RSD"
        };

        var cut = new RaiffeisenExpenseParser();

        // Act
        var result = cut.Parse("Koriscenje kartice 5356**9996\r\nDatum: 18.01.2024\r\nIznos: 1.430,70 RSD\r\nMesto: Mikromarket 110 Novi Sad RS\r\nStanje: ***");

        // Assert
        Assert.Equal(expected.Amount, result.Amount);
    }

    [Fact]
    public void AmountWithComma_ReturnsCorrectlyParsedAmount()
    {
        // Arrange
        var expected = new ParsedExpenseDto
        {
            Amount = 901.29m,
            Date = new DateOnly(2024, 1, 18),
            Place = "Mikromarket 110 Novi Sad RS",
            Currency = "RSD"
        };

        var cut = new RaiffeisenExpenseParser();

        // Act
        var result = cut.Parse("Koriscenje kartice 5356**9996\r\nDatum: 18.01.2024\r\nIznos: 901,29 RSD\r\nMesto: Mikromarket 110 Novi Sad RS\r\nStanje: ***");

        // Assert
        Assert.Equal(expected.Amount, result.Amount);
    }

    [Fact]
    public void CommonInput_ReturnsCorrectlyParsedValues()
    {
        // Arrange
        var expected = new ParsedExpenseDto
        {
            Amount = 901.29m,
            Date = new DateOnly(2024, 1, 18),
            Place = "Mikromarket 110 Novi Sad RS",
            Currency = "RSD"
        };

        var cut = new RaiffeisenExpenseParser();

        // Act
        var result = cut.Parse("Koriscenje kartice 5356**9996\r\nDatum: 18.01.2024\r\nIznos: 901,29 RSD\r\nMesto: Mikromarket 110 Novi Sad RS\r\nStanje: ***");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expected.Date, result.Date);
        Assert.Equal(expected.Place, result.Place);
        Assert.Equal(expected.Currency, result.Currency);
    }
}
