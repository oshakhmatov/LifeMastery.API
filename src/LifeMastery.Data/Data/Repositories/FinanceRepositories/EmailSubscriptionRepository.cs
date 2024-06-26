﻿using LifeMastery.Core.Modules.Finance.Models;
using LifeMastery.Core.Modules.Finance.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LifeMastery.Infrastructure.Data.Repositories.FinanceRepositories;

public class EmailSubscriptionRepository : RepositoryBase<EmailSubscription>, IEmailSubscriptionRepository
{
    public EmailSubscriptionRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<EmailSubscription?> Get(int id, CancellationToken token)
    {
        return await dbContext.EmailSubscriptions
            .Include(es => es.Rules)
            .Where(es => es.Id == id)
            .FirstOrDefaultAsync(cancellationToken: token);
    }

    public async Task<EmailSubscription[]> List(CancellationToken token)
    {
        return await dbContext.EmailSubscriptions
            .Include(es => es.Rules)
            .ToArrayAsync(cancellationToken: token);
    }

    public async Task<EmailSubscription[]> ListActive(CancellationToken token)
    {
        return await dbContext.EmailSubscriptions
            .Include(es => es.Rules)
            .ThenInclude(r => r.Category)
            .Where(es => es.IsActive)
            .ToArrayAsync(cancellationToken: token);
    }

    public async Task<EmailSubscription[]> ListActiveWithExpenses(CancellationToken token)
    {
        return await dbContext.EmailSubscriptions
            .Include(es => es.Expenses)
            .Include(es => es.Rules)
            .ThenInclude(r => r.Category)
            .Where(es => es.IsActive)
            .ToArrayAsync(cancellationToken: token);
    }
}
