using LifeMastery.Core.Modules.Finance.Models;

namespace LifeMastery.Core.Modules.Finance.DataTransferObjects;

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
