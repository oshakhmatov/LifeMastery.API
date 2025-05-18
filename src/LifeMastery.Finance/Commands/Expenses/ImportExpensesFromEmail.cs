using LifeMastery.Domain.Abstractions;
using LifeMastery.Finance.Services;

namespace LifeMastery.Finance.Commands.Expenses;

public sealed class ImportExpensesFromEmail(EmailHandler emailHandler) : ICommand
{
    public async Task Execute(CancellationToken token)
    {
        await emailHandler.HandleInbox(token);
    }
}
