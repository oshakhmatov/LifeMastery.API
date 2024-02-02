using LifeMastery.Core.Common;
using LifeMastery.Core.Modules.Finance.Repositories;

namespace LifeMastery.Core.Modules.Finance.Commands;

public sealed class RemovePaymentRequest
{
    public int RegularPaymentId { get; set; }
    public int PaymentId { get; set; }
}

public sealed class RemovePayment : CommandBase<RemovePaymentRequest>
{
    private readonly IRegularPaymentRepository regularPaymentRepository;

    public RemovePayment(IUnitOfWork unitOfWork, IRegularPaymentRepository regularPaymentRepository) : base(unitOfWork)
    {
        this.regularPaymentRepository = regularPaymentRepository;
    }

    protected override async Task OnExecute(RemovePaymentRequest request, CancellationToken token)
    {
        var regularPayment = await regularPaymentRepository.Get(request.RegularPaymentId, token)
            ?? throw new Exception($"Regular payment with ID='{request.RegularPaymentId}' was not found.");

        regularPayment.RemovePaymentById(request.PaymentId);
    }
}
