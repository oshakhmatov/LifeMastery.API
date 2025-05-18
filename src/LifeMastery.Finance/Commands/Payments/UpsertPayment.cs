using LifeMastery.Domain.Abstractions;
using LifeMastery.Finance.Models;

namespace LifeMastery.Finance.Commands.Payments;

public sealed class UpsertPayment(
    IRepository<RegularPayment> regularPayments,
    IUnitOfWork unitOfWork) : ICommand<UpsertPayment.Request>
{
    public async Task Execute(Request request, CancellationToken token)
    {
        var regularPayment = await regularPayments.GetByIdAsync(request.RegularPaymentId, token)
            ?? throw new AppException($"Regular payment with ID '{request.RegularPaymentId}' was not found.");

        var existingPayment = regularPayment.Payments
            .FirstOrDefault(p =>
                p.PeriodYear == request.PeriodYear &&
                p.PeriodMonth == request.PeriodMonth);

        if (existingPayment is not null && request.Id is null)
            throw new AppException("Payment for this period is already added.");

        var date = DateOnly.FromDateTime(request.Date);

        if (request.Id is not null)
        {
            var payment = regularPayment.GetPayment(request.Id.Value);
            payment.Date = date;
            payment.Amount = request.Amount;
            payment.PeriodYear = request.PeriodYear;
            payment.PeriodMonth = request.PeriodMonth;
        }
        else
        {
            regularPayment.AddPayment(new Payment(
                request.Amount,
                date,
                request.PeriodYear,
                request.PeriodMonth));
        }

        await unitOfWork.Commit(token);
    }

    public record Request(
        int? Id,
        int RegularPaymentId,
        decimal Amount,
        DateTime Date,
        int PeriodYear,
        int? PeriodMonth);
}
