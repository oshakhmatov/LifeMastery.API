using LifeMastery.Core.Modules.Finance.Repositories;
using LifeMastery.Core.Modules.Finance.Services.Abstractions;

namespace LifeMastery.Core.Modules.Finance.Services;

public class EmailHandler
{
    private readonly IEmailProvider emailProvider;
    private readonly IExpenseRepository expenseRepository;
    private readonly ExpenseParser expenseParser;
    private readonly IUnitOfWork unitOfWork;

    public EmailHandler(IEmailProvider emailProvider, ExpenseParser expenseParser, IExpenseRepository expenseRepository, IUnitOfWork unitOfWork)
    {
        this.emailProvider = emailProvider;
        this.expenseParser = expenseParser;
        this.expenseRepository = expenseRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task HandleInbox()
    {
        var messages = await emailProvider.GetMessages("Расходы");
        var contents = messages.SelectMany(m => m.AttachmentContents).ToArray();

        foreach(var content in contents)
        {
            var expense = await expenseParser.Parse(content);
            expenseRepository.Add(expense);
        }

        await unitOfWork.Commit();

        emailProvider.RemoveMessages("Расходы");
    }
}
