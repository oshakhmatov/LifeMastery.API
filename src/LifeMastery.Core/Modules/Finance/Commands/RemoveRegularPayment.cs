using LifeMastery.Core.Modules.Finance.Repositories;

namespace LifeMastery.Core.Modules.Finance.Commands;

public class RemoveRegularPayment
{
    private readonly IRegularPaymentRepository regularPaymentRepository;
    private readonly IUnitOfWork unitOfWork;

    public RemoveRegularPayment(
        IRegularPaymentRepository regularPaymentRepository,
        IUnitOfWork unitOfWork)
    {
        this.regularPaymentRepository = regularPaymentRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task Execute(int id)
    {
        var regularPayment = await regularPaymentRepository.Get(id);
        if (regularPayment is null)
            throw new Exception($"Regular payment with ID={id} was not found.");

        regularPaymentRepository.Remove(regularPayment);

        await unitOfWork.Commit();
    }
}
