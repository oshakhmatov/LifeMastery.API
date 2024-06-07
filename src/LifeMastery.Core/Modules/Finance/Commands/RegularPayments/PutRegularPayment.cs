using LifeMastery.Core.Common;
using LifeMastery.Core.Modules.Finance.Enums;
using LifeMastery.Core.Modules.Finance.Models;
using LifeMastery.Core.Modules.Finance.Repositories;

namespace LifeMastery.Core.Modules.Finance.Commands.RegularPayments;

public sealed class PutRegularPaymentRequest
{
    public int? Id { get; set; }
    public required string Name { get; set; }
    public decimal? Amount { get; set; }
    public required Period Period { get; set; }
    public int? DeadlineDay { get; set; }
    public int? DeadlineMonth { get; set; }
    public int? PayFromDay { get; set; }
    public bool IsAdvanced { get; set; }
    public bool IsTax { get; set; }
}

public sealed class PutRegularPayment(
    IUnitOfWork unitOfWork,
    IRegularPaymentRepository regularPaymentRepository) : CommandBase<PutRegularPaymentRequest>(unitOfWork)
{
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
            regularPayment.IsTax = request.IsTax;
            regularPayment.PayFromDay = request.PayFromDay;
            regularPayment.SetDeadline(request.DeadlineDay, request.DeadlineMonth);
        }
        else
        {
            regularPaymentRepository.Put(new RegularPayment(
                request.Name,
                request.IsAdvanced,
                request.IsTax,
                request.Period,
                request.DeadlineDay,
                request.DeadlineMonth,
                request.Amount,
                request.PayFromDay));
        }
    }
}