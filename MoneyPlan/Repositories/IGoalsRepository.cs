using System;
using System.Threading.Tasks;
using MoneyPlan.Models;

namespace MoneyPlan.Repositories;

public interface IGoalsRepository
{
    public Task AddMoneyGoal(MoneyGoal goal);

    public Task GetMoneyGoals(Guid accountId);
}