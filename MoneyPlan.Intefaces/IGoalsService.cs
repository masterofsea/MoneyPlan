using MoneyPlan.Domain.Models;

namespace MoneyPlan.Intefaces;

public interface IGoalsService
{
    public Task AddGoal(MoneyGoal goal);
    
    public Task UpdateGoal(MoneyGoal goal);
    
    public Task<IReadOnlyList<MoneyGoal>> GetGoals();
}