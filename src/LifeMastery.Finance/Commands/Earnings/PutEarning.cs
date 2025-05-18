using LifeMastery.Domain.Abstractions;
using LifeMastery.Finance.Models;

namespace LifeMastery.Finance.Commands.Earnings;

public sealed class UpdateEarningAmount(IRepository<Earning> earnings, IUnitOfWork unitOfWork) : ICommand<UpdateEarningAmount.Request>
{
    public async Task Execute(Request command, CancellationToken token)
    {
        var earning = await earnings.GetByIdAsync(command.Id, token)
            ?? throw new AppException($"Earning with ID '{command.Id}' was not found.");

        earning.Amount = command.Amount;

        await unitOfWork.Commit(token);
    }

    public record Request(int Id, decimal Amount);
}
