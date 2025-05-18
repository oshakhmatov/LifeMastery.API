using LifeMastery.Domain.Abstractions;
using LifeMastery.Finance.Models;

namespace LifeMastery.Finance.Commands.RegularPayments;

public sealed class RemoveRegularPayment(
    IRepository<RegularPayment> regularPayments,
    IUnitOfWork unitOfWork) : ICommand<RemoveRegularPayment.Request>
{
    public async Task Execute(Request request, CancellationToken token)
    {
        var payment = await regularPayments.GetByIdAsync(request.Id, token)
            ?? throw new AppException($"Regular payment with ID={request.Id} was not found.");

        regularPayments.Remove(payment);
        await unitOfWork.Commit(token);
    }

    public record Request(int Id);
}
