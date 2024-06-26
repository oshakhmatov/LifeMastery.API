﻿using LifeMastery.Core.Modules.Finance.Commands.EmailSubscriptions;
using LifeMastery.Core.Modules.Finance.Commands.ExpenseCategories;
using LifeMastery.Core.Modules.Finance.Commands.ExpenseCreationRules;
using LifeMastery.Core.Modules.Finance.Commands.Expenses;
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
        [FromServices] GetFinanceData getExpenses,
        [FromQuery] GetFinanceDataRequest request,
        CancellationToken cancellationToken)
    {
        return await getExpenses.Execute(request, cancellationToken);
    }

    [HttpPut("expenses")]
    public async Task PutExpense(
        [FromServices] PutExpense putExpense,
        [FromBody] PutExpenseRequest request,
        CancellationToken cancellationToken)
    {
        await putExpense.Execute(request, cancellationToken);
    }

    [HttpPost("expenses/update")]
    public async Task UpdateExpenses(
        [FromServices] UpdateExpenses updateExpenses,
        CancellationToken cancellationToken)
    {
        await updateExpenses.Execute(cancellationToken);
    }

    [HttpPost("expenses/load")]
    public async Task LoadExpenses(
        [FromServices] LoadExpenses loadExpenses,
        CancellationToken cancellationToken)
    {
        await loadExpenses.Execute(cancellationToken);
    }

    [HttpDelete("expenses/{id}")]
    public async Task RemoveExpense(
        [FromServices] RemoveExpense removeExpense,
        int id,
        CancellationToken cancellationToken)
    {
        await removeExpense.Execute(id, cancellationToken);
    }

    [HttpPut("expense-categories")]
    public async Task PutExpenseCategory(
       [FromServices] PutExpenseCategory putExpenseCategory,
       [FromBody] PutExpenseCategoryRequest request,
       CancellationToken cancellationToken)
    {
        await putExpenseCategory.Execute(request, cancellationToken);
    }

    [HttpDelete("expense-categories/{id}")]
    public async Task RemoveExpenseCategory(
        [FromServices] RemoveExpenseCategory removeExpenseCategory,
        int id,
        CancellationToken cancellationToken)
    {
        await removeExpenseCategory.Execute(id, cancellationToken);
    }

    [HttpPut("regular-payments")]
    public async Task PutRegularPayment(
        [FromServices] PutRegularPayment putRegularPayment,
        [FromBody] PutRegularPaymentRequest request,
        CancellationToken cancellationToken)
    {
        await putRegularPayment.Execute(request, cancellationToken);
    }

    [HttpDelete("regular-payments/{id}")]
    public async Task RemoveRegularPayment(
        [FromServices] RemoveRegularPayment removeRegularPayment,
        int id,
        CancellationToken cancellationToken)
    {
        await removeRegularPayment.Execute(id, cancellationToken);
    }

    [HttpPut("email-subscriptions")]
    public async Task PutEmailSubscription(
        [FromServices] PutEmailSubscription putEmailSubscription,
        [FromBody] PutEmailSubscriptionRequest request,
        CancellationToken cancellationToken)
    {
        await putEmailSubscription.Execute(request, cancellationToken);
    }

    [HttpDelete("email-subscriptions/{id}")]
    public async Task RemoveEmailSubscription(
        [FromServices] RemoveEmailSubscription removeEmailSubscription,
        int id,
        CancellationToken cancellationToken)
    {
        await removeEmailSubscription.Execute(id, cancellationToken);
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
}
