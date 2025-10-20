using LifeMastery.Domain.Abstractions;
using LifeMastery.Finance.Enums;
using LifeMastery.Finance.Models;

namespace LifeMastery.Finance.Commands.RegularPayments;

public sealed class UpsertRegularPayment(
    IRepository<RegularPayment> regularPayments,
    IUnitOfWork unitOfWork) : ICommand<UpsertRegularPayment.Request>
{
    public async Task Execute(Request request, CancellationToken token)
    {
        if (request.Id is not null)
        {
            var payment = await regularPayments.GetByIdAsync(request.Id.Value, token)
                ?? throw new AppException($"Regular payment with ID '{request.Id}' was not found.");

            payment.Period = request.Period;
            payment.Name = request.Name;
            payment.Amount = request.Amount;
            payment.IsAdvanced = request.IsAdvanced;
            payment.IsTax = request.IsTax;
            payment.IsActive = request.IsActive;
            payment.PayFromDay = request.PayFromDay;
            payment.SetDeadline(request.DeadlineDay, request.DeadlineMonth);
        }
        else
        {
            regularPayments.Add(new RegularPayment(
                request.Name,
                request.IsAdvanced,
                request.IsTax,
                request.IsActive,
                request.Period,
                request.DeadlineDay,
                request.DeadlineMonth,
                request.Amount,
                request.PayFromDay));
        }

        await unitOfWork.Commit(token);
    }

    public record Request(
        int? Id,
        string Name,
        decimal? Amount,
        Period Period,
        int? DeadlineDay,
        int? DeadlineMonth,
        int? PayFromDay,
        bool IsAdvanced,
        bool IsTax,
        bool IsActive);
}
