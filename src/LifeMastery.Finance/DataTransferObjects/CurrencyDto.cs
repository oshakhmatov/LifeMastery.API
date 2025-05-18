using LifeMastery.Finance.Models;

namespace LifeMastery.Finance.DataTransferObjects;

public class CurrencyDto
{
    public required int Id { get; init; }
    public required string Name { get; init; }

    public static CurrencyDto FromModel(Currency currency)
    {
        return new CurrencyDto
        {
            Id = currency.Id,
            Name = currency.Name
        };
    }
}
