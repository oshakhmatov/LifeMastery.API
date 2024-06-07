using LifeMastery.Core.Common;
using LifeMastery.Core.Modules.Finance.Repositories;

namespace LifeMastery.Core.Modules.Finance.Commands.Payments;

public sealed class RemovePaymentRequest
{
    public required int RegularPaymentId { get; set; }
    public required int PaymentId { get; set; }
}

public sealed class RemovePayment(
    IUnitOfWork unitOfWork,
    IRegularPaymentRepository regularPaymentRepository) : CommandBase<RemovePaymentRequest>(unitOfWork)
{
    protected override async Task OnExecute(RemovePaymentRequest request, CancellationToken token)
    {
        var regularPayment = await regularPaymentRepository.Get(request.RegularPaymentId, token)
            ?? throw new Exception($"Regular payment with ID='{request.RegularPaymentId}' was not found.");

        regularPayment.RemovePaymentById(request.PaymentId);
    }
}
