using LifeMastery.Finance.DataTransferObjects;
using LifeMastery.Finance.Services;
using LifeMastery.Infrastructure.Services;

namespace UnitTests.Tests.Finance.Services.ExpenseParsers;

public class RaiffeisenExpenseParserTests
{
    [Fact]
    public void AmountWithCommaAndDot_ReturnsCorrectlyParsedAmount()
    {
        var expected = new ParsedExpenseDto
        {
            Amount = 1430.70m,
            Date = new DateOnly(2024, 1, 18),
            Place = "Mikromarket 110 Novi Sad RS",
            Currency = "RSD",
            TransactionId = "367114926249",
            Source = @"<Stavke DatumValute=""18.01.2024"" 
                    NalogKorisnik=""Mikromarket 110 Novi Sad RS"" 
                    Opis=""535683******9996 / Iznos transakcije: 1.430,70 u valuti RSD"" 
                    Referenca=""367114926249"" />"
        };

        var cut = new RaiffeisenExpenseParser(new RsCultureProvider());

        var xmlContent = @"
        <TransakcioniRacunPrivredaPromet>
            <Stavke DatumValute=""18.01.2024"" 
                    NalogKorisnik=""Mikromarket 110 Novi Sad RS"" 
                    Opis=""535683******9996 / Iznos transakcije: 1.430,70 u valuti RSD"" 
                    Referenca=""367114926249"" />
        </TransakcioniRacunPrivredaPromet>";

        var result = cut.Parse(xmlContent);

        Assert.NotNull(result);
        Assert.Equal(expected.Amount, result[0].Amount);
        Assert.Equal(expected.Date, result[0].Date);
        Assert.Equal(expected.Place, result[0].Place);
        Assert.Equal(expected.Currency, result[0].Currency);
        Assert.Equal(expected.TransactionId, result[0].TransactionId);
    }

    [Fact]
    public void AmountWithDot_ReturnsCorrectlyParsedAmount()
    {
        var expected = new ParsedExpenseDto
        {
            Amount = 430.70m,
            Date = new DateOnly(2024, 1, 18),
            Place = "Mikromarket 110 Novi Sad RS",
            Currency = "RSD",
            TransactionId = "367114926249",
            Source = @"<Stavke DatumValute=""18.01.2024"" 
                    NalogKorisnik=""Mikromarket 110 Novi Sad RS"" 
                    Opis=""535683******9996 / Iznos transakcije: 430.70 u valuti RSD"" 
                    Referenca=""367114926249"" />"
        };

        var cut = new RaiffeisenExpenseParser(new RsCultureProvider());

        var xmlContent = @"
        <TransakcioniRacunPrivredaPromet>
            <Stavke DatumValute=""18.01.2024"" 
                    NalogKorisnik=""Mikromarket 110 Novi Sad RS"" 
                    Opis=""535683******9996 / Iznos transakcije: 430.70 u valuti RSD"" 
                    Referenca=""367114926249"" />
        </TransakcioniRacunPrivredaPromet>";

        var result = cut.Parse(xmlContent);

        Assert.NotNull(result);
        Assert.Equal(expected.Amount, result[0].Amount);
        Assert.Equal(expected.Date, result[0].Date);
        Assert.Equal(expected.Place, result[0].Place);
        Assert.Equal(expected.Currency, result[0].Currency);
        Assert.Equal(expected.TransactionId, result[0].TransactionId);
    }

    [Fact]
    public void AmountWithComma_ReturnsCorrectlyParsedAmount()
    {
        var expected = new ParsedExpenseDto
        {
            Amount = 901.29m,
            Date = new DateOnly(2024, 1, 18),
            Place = "Mikromarket 110 Novi Sad RS",
            Currency = "RSD",
            TransactionId = "367114926250",
            Source = @"<Stavke DatumValute=""18.01.2024"" 
                    NalogKorisnik=""Mikromarket 110 Novi Sad RS"" 
                    Opis=""535683******9996 / Iznos transakcije: 901,29 u valuti RSD"" 
                    Referenca=""367114926250"" />"
        };

        var cut = new RaiffeisenExpenseParser(new RsCultureProvider());

        var xmlContent = @"
        <TransakcioniRacunPrivredaPromet>
            <Stavke DatumValute=""18.01.2024"" 
                    NalogKorisnik=""Mikromarket 110 Novi Sad RS"" 
                    Opis=""535683******9996 / Iznos transakcije: 901,29 u valuti RSD"" 
                    Referenca=""367114926250"" />
        </TransakcioniRacunPrivredaPromet>";

        var result = cut.Parse(xmlContent);

        Assert.NotNull(result);
        Assert.Equal(expected.Amount, result[0].Amount);
        Assert.Equal(expected.Date, result[0].Date);
        Assert.Equal(expected.Place, result[0].Place);
        Assert.Equal(expected.Currency, result[0].Currency);
        Assert.Equal(expected.TransactionId, result[0].TransactionId);
    }

    [Fact]
    public void CommonInput_ReturnsCorrectlyParsedValues()
    {
        var expected = new ParsedExpenseDto
        {
            Amount = 901.29m,
            Date = new DateOnly(2024, 1, 18),
            Place = "Mikromarket 110 Novi Sad RS",
            Currency = "RSD",
            TransactionId = "367114926251",
            Source = @"<Stavke DatumValute=""18.01.2024"" 
                    NalogKorisnik=""Mikromarket 110 Novi Sad RS"" 
                    Opis=""535683******9996 / Iznos transakcije: 901,29 у valuti RSD"" 
                    Referenca=""367114926251"" />"
        };

        var cut = new RaiffeisenExpenseParser(new RsCultureProvider());

        var xmlContent = @"
        <TransakcioniRacunPrivredaPromet>
            <Stavke DatumValute=""18.01.2024"" 
                    NalogKorisnik=""Mikromarket 110 Novi Sad RS"" 
                    Opis=""535683******9996 / Iznos transakcije: 901,29 у valuti RSD"" 
                    Referenca=""367114926251"" />
        </TransakcioniRacunPrivredaPromet>";

        var result = cut.Parse(xmlContent);

        Assert.NotNull(result);
        Assert.Equal(expected.Amount, result[0].Amount);
        Assert.Equal(expected.Date, result[0].Date);
        Assert.Equal(expected.Place, result[0].Place);
        Assert.Equal(expected.Currency, result[0].Currency);
        Assert.Equal(expected.TransactionId, result[0].TransactionId);
    }
}
