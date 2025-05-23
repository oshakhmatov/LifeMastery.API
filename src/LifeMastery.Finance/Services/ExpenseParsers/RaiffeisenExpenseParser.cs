﻿using LifeMastery.Domain.Abstractions;
using LifeMastery.Finance.DataTransferObjects;
using LifeMastery.Finance.Services.Abstractions;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace LifeMastery.Finance.Services;

public sealed partial class RaiffeisenExpenseParser(IAppCultureProvider cultureProvider) : IExpenseParser
{
    public ParsedExpenseDto[] Parse(string content)
    {
        var doc = XDocument.Parse(content);

        return doc.Descendants("Stavke")
            .Select(stavka =>
            {
                var dateStr = (string?)stavka.Attribute("DatumValute") ?? "";
                var place = CleanString((string?)stavka.Attribute("NalogKorisnik") ?? "");
                var opis = (string?)stavka.Attribute("Opis") ?? "";
                var paymentCode = (string?)stavka.Attribute("SifraPlacanja") ?? null;
                var transactionId = (string?)stavka.Attribute("Referenca") ?? "";

                if (string.IsNullOrEmpty(transactionId)
                    || place.Contains("RAIFFEISEN", StringComparison.CurrentCultureIgnoreCase)
                    || paymentCode == "286"
                    || place.Contains("SBB DOO", StringComparison.CurrentCultureIgnoreCase))
                    return null;

                var amountMatch = AmountRegex().Match(opis);
                if (!amountMatch.Success)
                {
                    return null;
                }

                var amountString = NormalizeAmount(amountMatch.Groups[1].Value);
                if (!decimal.TryParse(amountString, NumberStyles.AllowDecimalPoint, cultureProvider.CurrentCulture, out var amount))
                    return null;

                var currencyMatch = CurrencyRegex().Match(opis);
                var currency = currencyMatch.Success ? currencyMatch.Groups[1].Value : "RSD";

                var date = DateOnly.ParseExact(dateStr, "dd.MM.yyyy", cultureProvider.CurrentCulture);

                return new ParsedExpenseDto(
                    Date: date,
                    Place: place,
                    Amount: amount,
                    Currency: currency,
                    TransactionId: transactionId,
                    Source: stavka.ToString());
            })
            .Where(expense => expense != null)
            .ToArray()!;
    }

    private static string NormalizeAmount(string rawAmount)
    {
        var cleaned = ThousandsSeparatorRegex().Replace(rawAmount, "");
        return DecimalSeparatorRegex().Replace(cleaned, ",");
    }

    private static string CleanString(string input) => input.TrimEnd('\r');

    [GeneratedRegex(@"(?:Iznos transakcije:\s*|EUR\s)([\d,.]+)")]
    private static partial Regex AmountRegex();

    [GeneratedRegex(@"Iznos transakcije:\s*[\d,.]+\s*u\s*valuti\s*([A-Z]+)")]
    private static partial Regex CurrencyRegex();

    [GeneratedRegex(@"(?<=\d)[.,](?=\d{3}([.,]\d{2})?$)")]
    private static partial Regex ThousandsSeparatorRegex();

    [GeneratedRegex(@"([.,])(?=\d{2}$)")]
    private static partial Regex DecimalSeparatorRegex();
}
