using LifeMastery.Core.Modules.Finance.DataTransferObjects;

namespace LifeMastery.Core.Modules.Finance.Services.Abstractions;

public interface IExpenseParser
{
    public ParsedExpenseDto? Parse(string content);
}
