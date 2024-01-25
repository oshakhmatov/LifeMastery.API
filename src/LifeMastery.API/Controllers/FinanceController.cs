using LifeMastery.Core.Modules.Finance.Commands;
using LifeMastery.Core.Modules.Finance.DataTransferObjects;
using LifeMastery.Core.Modules.Finance.Queries;
using Microsoft.AspNetCore.Mvc;

namespace LifeMastery.API.Controllers;

[Route("api/finance")]
public class FinanceController : ApiControllerBase
{
    [HttpGet]
    public async Task<FinanceViewModel> GetFinanceData(
        [FromServices] GetFinanceData getExpenses)
    {
        return await getExpenses.Execute();
    }

    [HttpPut("expenses")]
    public async Task<IActionResult> PutExpense(
        [FromServices] PutExpense putExpense,
        [FromBody] PutExpenseRequest request)
    {
        await putExpense.Execute(request);
        return Ok();
    }

    [HttpDelete("expenses/{id}")]
    public async Task RemoveExpense(
        [FromServices] RemoveExpense removeExpense,
        int id)
    {
        await removeExpense.Execute(id);
    }

    [HttpPut("expense-categories")]
    public async Task PutExpenseCategory(
       [FromServices] PutExpenseCategory putExpenseCategory,
       [FromBody] PutExpenseCategoryRequest request)
    {
        await putExpenseCategory.Execute(request);
    }

    [HttpDelete("expense-categories/{id}")]
    public async Task RemoveExpenseCategory(
        [FromServices] RemoveExpenseCategory removeExpenseCategory,
        int id)
    {
        await removeExpenseCategory.Execute(id);
    }

    [HttpPut("regular-payments")]
    public async Task PutRegularPayment(
        [FromServices] PutRegularPayment putRegularPayment,
        [FromBody] PutRegularPaymentRequest request)
    {
        await putRegularPayment.Execute(request);
    }

    [HttpDelete("regular-payments/{id}")]
    public async Task RemoveRegularPayment(
        [FromServices] RemoveRegularPayment removeRegularPayment,
        int id)
    {
        await removeRegularPayment.Execute(id);
    }

    [HttpPut("email-subscriptions")]
    public async Task PutEmailSubscription(
        [FromServices] PutEmailSubscription putEmailSubscription,
        [FromBody] PutEmailSubscriptionRequest request)
    {
        await putEmailSubscription.Execute(request);
    }

    [HttpDelete("email-subscriptions/{id}")]
    public async Task RemoveEmailSubscription(
        [FromServices] RemoveEmailSubscription removeEmailSubscription,
        int id)
    {
        await removeEmailSubscription.Execute(id);
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
}
