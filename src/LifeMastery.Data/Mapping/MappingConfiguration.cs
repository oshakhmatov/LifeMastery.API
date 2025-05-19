using LifeMastery.Finance.DataTransferObjects;
using LifeMastery.Finance.Models;
using LifeMastery.Health.DataTransferObjects;
using LifeMastery.Health.Models;
using Mapster;
using System.Globalization;

namespace LifeMastery.Infrastructure.Mapping;

public static class MappingConfiguration
{
    public static void Register()
    {
        TypeAdapterConfig<Payment, PaymentDto>.NewConfig()
            .Map(dest => dest.PeriodName, src => src.GetPeriodName());

        TypeAdapterConfig<RegularPayment, RegularPaymentDto>.NewConfig()
            .Map(dest => dest.ApproximateAmount, src => src.GetApproximateAmount())
            .Map(dest => dest.IsPaid, src => src.IsPaid());

        TypeAdapterConfig<ExpenseCreationRule, ExpenseCreationRuleDto>.NewConfig()
            .Map(dest => dest.CategoryId, src => src.Category.Id)
            .Map(dest => dest.CategoryName, src => src.Category.Name);

        TypeAdapterConfig<EmailSubscription, EmailSubscriptionDto>.NewConfig()
            .Map(dest => dest.Rules, src =>
                src.Rules
                   .OrderBy(r => r.Place)
                   .Adapt<ExpenseCreationRuleDto[]>());

        TypeAdapterConfig<FamilyBudgetRule, FamilyBudgetRuleDto>.NewConfig()
            .Map(dest => dest.ContributionRatio, src => src.GetContributionRatioName());

        TypeAdapterConfig<Earning, EarningDto>.NewConfig()
            .Map(dest => dest.FamilyMemberName, src => src.FamilyMember.Name);

        TypeAdapterConfig<Expense, ExpenseDto>.NewConfig()
            .Map(dest => dest.CategoryId, src => src.Category!.Id)
            .Map(dest => dest.CategoryName, src => src.Category!.Name)
            .Map(dest => dest.CurrencyId, src => src.Currency!.Id)
            .Map(dest => dest.Source, src => src.ParsedPlace);

        TypeAdapterConfig<ExpenseCategory, ExpenseCategoryDto>.NewConfig()
            .Map(dest => dest.FamilyMemberId, src => src.FamilyMember!.Id);

        TypeAdapterConfig<WeightRecord, WeightRecordDto>.NewConfig()
            .Map(dest => dest.Date, src => src.Date.ToString("dd.MM.yyyy", new CultureInfo("ru-RU")));
    }
}
