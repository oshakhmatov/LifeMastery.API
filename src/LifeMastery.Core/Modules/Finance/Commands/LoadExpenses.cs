using LifeMastery.Core.Modules.Finance.Services;

namespace LifeMastery.Core.Modules.Finance.Commands;

public class LoadExpenses
{
    private readonly EmailHandler emailHandler;

    public LoadExpenses(EmailHandler emailHandler)
    {
        this.emailHandler = emailHandler;
    }

    public async Task Execute(CancellationToken cancellationToken)
    {
        await emailHandler.HandleInbox(cancellationToken);
    }
}
