using LifeMastery.Domain.Abstractions;
using LifeMastery.Finance.Models;

namespace LifeMastery.Finance.Commands.ExpenseCategories;

public sealed class UpsertExpenseCategory(
    IRepository<ExpenseCategory> categories,
    IRepository<FamilyMember> familyMembers,
    IUnitOfWork unitOfWork) : ICommand<UpsertExpenseCategory.Request>
{
    public async Task Execute(Request request, CancellationToken token)
    {
        if (request.Id is not null)
        {
            var category = await categories.GetByIdAsync(request.Id.Value, token)
                ?? throw new AppException($"Expense category with ID '{request.Id}' was not found.");

            category.Name = request.Name;
            category.IsFood = request.IsFood;
            category.Color = request.Color;

            category.FamilyMember = request.FamilyMemberId is not null
                ? await GetFamilyMember(request.FamilyMemberId.Value, token)
                : null;
        }
        else
        {
            var existing = await categories.FirstOrDefaultAsync(c => c.Name == request.Name, token);
            if (existing is not null)
                throw new AppException($"Expense category with name '{request.Name}' already exists.");

            var category = new ExpenseCategory(request.Name, request.IsFood)
            {
                Color = request.Color,
                FamilyMember = request.FamilyMemberId is not null
                    ? await GetFamilyMember(request.FamilyMemberId.Value, token)
                    : null
            };

            categories.Add(category);
        }

        await unitOfWork.Commit(token);
    }

    private async Task<FamilyMember> GetFamilyMember(int id, CancellationToken token)
    {
        return await familyMembers.GetByIdAsync(id, token)
            ?? throw new AppException($"Family member with ID '{id}' was not found.");
    }

    public record Request(
        int? Id,
        string Name,
        bool IsFood,
        string? Color,
        int? FamilyMemberId);
}
