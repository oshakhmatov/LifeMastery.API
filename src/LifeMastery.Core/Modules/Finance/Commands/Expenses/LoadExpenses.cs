using LifeMastery.Core.Modules.Finance.Services;

namespace LifeMastery.Core.Modules.Finance.Commands.Expenses;

public class LoadExpenses(EmailHandler emailHandler)
{
    public async Task Execute(CancellationToken cancellationToken)
    {
        await emailHandler.HandleInbox(cancellationToken);
    }
}
