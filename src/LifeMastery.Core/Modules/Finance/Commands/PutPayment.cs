using LifeMastery.Core.Common;
using LifeMastery.Core.Modules.Finance.Models;
using LifeMastery.Core.Modules.Finance.Repositories;

namespace LifeMastery.Core.Modules.Finance.Commands;

public sealed class PutPaymentRequest
{
    public int RegularPaymentId { get; set; }
    public int? Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public int PeriodYear { get; set; }
    public int? PeriodMonth { get; set; }
}

public sealed class PutPayment : CommandBase<PutPaymentRequest>
{
    private readonly IRegularPaymentRepository regularPaymentRepository;

    public PutPayment(IUnitOfWork unitOfWork, IRegularPaymentRepository regularPaymentRepository) : base(unitOfWork)
    {
        this.regularPaymentRepository = regularPaymentRepository;
    }

    protected override async Task OnExecute(PutPaymentRequest request, CancellationToken token)
    {
        var regularPayment = await regularPaymentRepository.Get(request.RegularPaymentId, token)
           ?? throw new Exception($"Regular payment with ID='{request.RegularPaymentId}' was not found.");

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