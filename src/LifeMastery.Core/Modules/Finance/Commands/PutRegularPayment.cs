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
    public bool IsAdvanced { get; set; }
}

public sealed class PutRegularPayment
{
    private readonly IRegularPaymentRepository regularPaymentRepository;
    private readonly IUnitOfWork unitOfWork;

    public PutRegularPayment(
        IRegularPaymentRepository regularPaymentRepository,
        IUnitOfWork unitOfWork)
    {
        this.regularPaymentRepository = regularPaymentRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task Execute(PutRegularPaymentRequest request)
    {
        if (request.Id.HasValue)
        {
            var regularPayment = await regularPaymentRepository.Get(request.Id.Value);
            if (regularPayment == null)
                throw new Exception($"Regular payment with ID '{request.Id}' was not found.");

            regularPayment.Period = request.Period;
            regularPayment.Name = request.Name;
            regularPayment.Amount = request.Amount;
            regularPayment.SetDeadline(request.DeadlineDay, request.DeadlineMonth);

            regularPaymentRepository.Update(regularPayment);
        }
        else
        {
            regularPaymentRepository.Add(new RegularPayment(
                request.Name,
                request.IsAdvanced,
                request.Period,
                request.DeadlineDay,
                request.DeadlineMonth,
                request.Amount));
        }

        await unitOfWork.Commit();
    }
}