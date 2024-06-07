using LifeMastery.Core.Common;
using LifeMastery.Core.Modules.Finance.Repositories;

namespace LifeMastery.Core.Modules.Finance.Commands.RegularPayments;

public sealed class RemoveRegularPayment(
    IUnitOfWork unitOfWork,
    IRegularPaymentRepository regularPaymentRepository) : CommandBase<int>(unitOfWork)
{
    protected override async Task OnExecute(int id, CancellationToken token)
    {
        var regularPayment = await regularPaymentRepository.Get(id, token)
            ?? throw new Exception($"Regular payment with ID={id} was not found.");

        regularPaymentRepository.Remove(regularPayment);
    }
}
