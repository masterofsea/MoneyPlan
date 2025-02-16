using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MoneyPlan.Models;

namespace MoneyPlan.Services;

public interface IGoalsService
{
    public Task AddGoal(MoneyGoal goal);
    
    public Task UpdateGoal(MoneyGoal goal);
    
    public Task<IReadOnlyList<MoneyGoal>> GetGoals(Guid accountId);
}