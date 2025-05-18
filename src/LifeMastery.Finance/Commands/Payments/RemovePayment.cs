using LifeMastery.Domain.Abstractions;
using LifeMastery.Finance.Models;

namespace LifeMastery.Finance.Commands.Payments;

public sealed class RemovePayment(
    IRepository<RegularPayment> regularPayments,
    IUnitOfWork unitOfWork) : ICommand<RemovePayment.Request>
{
    public async Task Execute(Request request, CancellationToken token)
    {
        var regularPayment = await regularPayments.GetByIdAsync(request.RegularPaymentId, token)
            ?? throw new AppException($"Regular payment with ID '{request.RegularPaymentId}' was not found.");

        regularPayment.RemovePaymentById(request.PaymentId);
        await unitOfWork.Commit(token);
    }

    public record Request(int RegularPaymentId, int PaymentId);
}
