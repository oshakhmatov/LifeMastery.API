using LifeMastery.Core.Common;
using LifeMastery.Core.Modules.Finance.Enums;
using LifeMastery.Core.Modules.Finance.Models;
using LifeMastery.Core.Modules.Finance.Repositories;

namespace LifeMastery.Core.Modules.Finance.Commands;

public sealed class PutRegularPaymentRequest
{
    public int? Id { get; set; }
    public string Name { get; set; }
    public decimal? Amount { get; set; }
    public Period Period { get; set; }
    public int? DeadlineDay { get; set; }
    public int? DeadlineMonth { get; set; }
    public int? PayFromDay { get; set; }
    public bool IsAdvanced { get; set; }
}

public sealed class PutRegularPayment : CommandBase<PutRegularPaymentRequest>
{
    private readonly IRegularPaymentRepository regularPaymentRepository;

    public PutRegularPayment(IUnitOfWork unitOfWork, IRegularPaymentRepository regularPaymentRepository) : base(unitOfWork)
    {
        this.regularPaymentRepository = regularPaymentRepository;
    }

    protected override async Task OnExecute(PutRegularPaymentRequest request, CancellationToken token)
    {
        if (request.Id.HasValue)
        {
            var regularPayment = await regularPaymentRepository.Get(request.Id.Value, token)
                ?? throw new Exception($"Regular payment with ID '{request.Id}' was not found.");

            regularPayment.Period = request.Period;
            regularPayment.Name = request.Name;
            regularPayment.Amount = request.Amount;
            regularPayment.IsAdvanced = request.IsAdvanced;
            regularPayment.PayFromDay = request.PayFromDay;
            regularPayment.SetDeadline(request.DeadlineDay, request.DeadlineMonth);

            regularPaymentRepository.Put(regularPayment);
        }
        else
        {
            regularPaymentRepository.Put(new RegularPayment(
                request.Name,
                request.IsAdvanced,
                request.Period,
                request.DeadlineDay,
                request.DeadlineMonth,
                request.Amount,
                request.PayFromDay));
        }
    }
}