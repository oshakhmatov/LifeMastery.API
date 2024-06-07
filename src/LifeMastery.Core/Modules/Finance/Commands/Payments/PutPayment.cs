using LifeMastery.Core.Common;
using LifeMastery.Core.Modules.Finance.Models;
using LifeMastery.Core.Modules.Finance.Repositories;

namespace LifeMastery.Core.Modules.Finance.Commands.Payments;

public sealed class PutPaymentRequest
{
    public int? Id { get; set; }
    public required int RegularPaymentId { get; set; }
    public required decimal Amount { get; set; }
    public required DateTime Date { get; set; }
    public required int PeriodYear { get; set; }
    public int? PeriodMonth { get; set; }
}

public sealed class PutPayment(
    IUnitOfWork unitOfWork,
    IRegularPaymentRepository regularPaymentRepository) : CommandBase<PutPaymentRequest>(unitOfWork)
{
    protected override async Task OnExecute(PutPaymentRequest request, CancellationToken token)
    {
        var regularPayment = await regularPaymentRepository.Get(request.RegularPaymentId, token)
           ?? throw new Exception($"Regular payment with ID='{request.RegularPaymentId}' was not found.");

        var paymentForThePeriod = regularPayment.Payments
            .Where(p => p.PeriodYear == request.PeriodYear)
            .Where(p => p.PeriodYear == request.PeriodMonth)
            .FirstOrDefault();

        if (paymentForThePeriod is not null)
            throw new Exception($"Payment for this period is already added.");

        var date = DateOnly.FromDateTime(request.Date);
        if (request.Id.HasValue)
        {
            var payment = regularPayment.GetPayment(request.Id.Value);

            payment.Date = date;
            payment.Amount = request.Amount;
            payment.PeriodYear = request.PeriodYear;
            payment.PeriodMonth = request.PeriodMonth;
        }
        else
        {
            regularPayment.AddPayment(new Payment(request.Amount, date, request.PeriodYear, request.PeriodMonth));
        }
    }
}