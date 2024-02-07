using LifeMastery.Core.Common;
using LifeMastery.Core.Modules.Finance.Repositories;

namespace LifeMastery.Core.Modules.Finance.Commands;

public class RemoveRegularPayment : CommandBase<int>
{
    private readonly IRegularPaymentRepository regularPaymentRepository;

    public RemoveRegularPayment(
        IUnitOfWork unitOfWork,
        IRegularPaymentRepository regularPaymentRepository) : base(unitOfWork)
    {
        this.regularPaymentRepository = regularPaymentRepository;
    }

    protected override async Task OnExecute(int id, CancellationToken token)
    {
        var regularPayment = await regularPaymentRepository.Get(id, token)
            ?? throw new Exception($"Regular payment with ID={id} was not found.");

        regularPaymentRepository.Remove(regularPayment);
    }
}
