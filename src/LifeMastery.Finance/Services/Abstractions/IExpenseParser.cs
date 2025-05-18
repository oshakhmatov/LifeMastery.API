using LifeMastery.Finance.DataTransferObjects;

namespace LifeMastery.Finance.Services.Abstractions;

public interface IExpenseParser
{
    public ParsedExpenseDto[] Parse(string content);
}
