using LifeMastery.Core.Modules.Finance.Commands.Currencies;
using LifeMastery.Core.Modules.Finance.Commands.Earnings;
using LifeMastery.Core.Modules.Finance.Commands.EmailSubscriptions;
using LifeMastery.Core.Modules.Finance.Commands.ExpenseCategories;
using LifeMastery.Core.Modules.Finance.Commands.ExpenseCreationRules;
using LifeMastery.Core.Modules.Finance.Commands.Expenses;
using LifeMastery.Core.Modules.Finance.Commands.FamilyMembers;
using LifeMastery.Core.Modules.Finance.Commands.Info;
using LifeMastery.Core.Modules.Finance.Commands.Payments;
using LifeMastery.Core.Modules.Finance.Commands.RegularPayments;
using LifeMastery.Core.Modules.Finance.DataTransferObjects;
using LifeMastery.Core.Modules.Finance.Queries;
using Microsoft.AspNetCore.Mvc;

namespace LifeMastery.API.Controllers;

[Route("api/finance")]
public class FinanceController : ApiControllerBase
{
    [HttpGet]
    public async Task<FinanceViewModel> GetFinanceData(
        [FromServices] GetFinanceData getFinanceData,
        [FromQuery] GetFinanceDataRequest request,
        CancellationToken token)
    {
        return await getFinanceData.Execute(request, token);
    }

    [HttpPut("expenses")]
    public async Task PutExpense(
        [FromServices] PutExpense putExpense,
        [FromBody] PutExpenseRequest request,
        CancellationToken token)
    {
        await putExpense.Execute(request, token);
    }

    [HttpPost("expenses/update")]
    public async Task UpdateExpenses(
        [FromServices] UpdateExpenses updateExpenses,
        CancellationToken token)
    {
        await updateExpenses.Execute(token);
    }

    [HttpPost("expenses/load")]
    public async Task LoadExpenses(
        [FromServices] LoadExpenses loadExpenses,
        CancellationToken token)
    {
        await loadExpenses.Execute(token);
    }

    [HttpDelete("expenses/{id}")]
    public async Task RemoveExpense(
        [FromServices] RemoveExpense removeExpense,
        int id,
        CancellationToken token)
    {
        await removeExpense.Execute(id, token);
    }

    [HttpPut("expense-categories")]
    public async Task PutExpenseCategory(
       [FromServices] PutExpenseCategory putExpenseCategory,
       [FromBody] PutExpenseCategoryRequest request,
       CancellationToken token)
    {
        await putExpenseCategory.Execute(request, token);
    }

    [HttpDelete("expense-categories/{id}")]
    public async Task RemoveExpenseCategory(
        [FromServices] RemoveExpenseCategory removeExpenseCategory,
        int id,
        CancellationToken token)
    {
        await removeExpenseCategory.Execute(id, token);
    }

    [HttpPut("regular-payments")]
    public async Task PutRegularPayment(
        [FromServices] PutRegularPayment putRegularPayment,
        [FromBody] PutRegularPaymentRequest request,
        CancellationToken token)
    {
        await putRegularPayment.Execute(request, token);
    }

    [HttpDelete("regular-payments/{id}")]
    public async Task RemoveRegularPayment(
        [FromServices] RemoveRegularPayment removeRegularPayment,
        int id,
        CancellationToken token)
    {
        await removeRegularPayment.Execute(id, token);
    }

    [HttpPut("email-subscriptions")]
    public async Task PutEmailSubscription(
        [FromServices] PutEmailSubscription putEmailSubscription,
        [FromBody] PutEmailSubscriptionRequest request,
        CancellationToken token)
    {
        await putEmailSubscription.Execute(request, token);
    }

    [HttpDelete("email-subscriptions/{id}")]
    public async Task RemoveEmailSubscription(
        [FromServices] RemoveEmailSubscription removeEmailSubscription,
        int id,
        CancellationToken token)
    {
        await removeEmailSubscription.Execute(id, token);
    }

    [HttpPut("expense-creation-rules")]
    public async Task PutExpenseCreationRule(
        [FromServices] PutExpenseCreationRule putExpenseCreationRule,
        [FromBody] PutExpenseCreationRuleRequest request,
        CancellationToken token)
    {
        await putExpenseCreationRule.Execute(request, token);
    }

    [HttpDelete("expense-creation-rules")]
    public async Task RemoveEmailSubscription(
        [FromServices] RemoveExpenseCreationRule removeExpenseCreationRule,
        [FromBody] RemoveExpenseCreationRuleRequest request,
        CancellationToken token)
    {
        await removeExpenseCreationRule.Execute(request, token);
    }

    [HttpPut("payments")]
    public async Task PutPayment(
        [FromServices] PutPayment putPayment,
        [FromBody] PutPaymentRequest request,
        CancellationToken token)
    {
        await putPayment.Execute(request, token);
    }

    [HttpDelete("payments")]
    public async Task RemovePayment(
        [FromServices] RemovePayment removePayment,
        [FromBody] RemovePaymentRequest request,
        CancellationToken token)
    {
        await removePayment.Execute(request, token);
    }

    [HttpPut("info")]
    public async Task PutStatistics(
        [FromServices] PutFinanceInfo putFinanceInfo,
        [FromBody] PutFinanceInfoCommand command,
        CancellationToken token)
    {
        await putFinanceInfo.Execute(command, token);
    }

    [HttpPut("currencies")]
    public async Task PutCurrency(
        [FromServices] PutCurrency putCurrency,
        [FromBody] PutCurrencyRequest command,
        CancellationToken token)
    {
        await putCurrency.Execute(command, token);
    }

    [HttpDelete("currencies/{id}")]
    public async Task RemoveCurrency(
        [FromServices] RemoveCurrency removeCurrency,
        int id,
        CancellationToken token)
    {
        await removeCurrency.Execute(id, token);
    }

    [HttpPut("family-members")]
    public async Task PutFamilyMember(
        [FromServices] PutFamilyMember putFamilyMember,
        [FromBody] PutFamilyMemberRequest command,
        CancellationToken token)
    {
        await putFamilyMember.Execute(command, token);
    }

    [HttpDelete("family-members/{id}")]
    public async Task RemoveFamilyMember(
        [FromServices] RemoveFamilyMember removeFamilyMember,
        int id,
        CancellationToken token)
    {
        await removeFamilyMember.Execute(id, token);
    }

    [HttpPut("earnings")]
    public async Task PutEarning(
        [FromServices] PutEarning putEarning,
        [FromBody] PutEarningRequest command,
        CancellationToken token)
    {
        await putEarning.Execute(command, token);
    }

    [HttpPut("family-budget-rules")]
    public async Task PutFamilyBudgetRule(
        [FromServices] PutFamilyBudgetRule putFamilyBudget,
        [FromBody] PutFamilyBudgetRuleRequest command,
        CancellationToken token)
    {
        await putFamilyBudget.Execute(command, token);
    }
}
