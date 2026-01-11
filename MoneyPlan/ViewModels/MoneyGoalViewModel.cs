using MoneyPlan.Domain.Models;

namespace MoneyPlan.ViewModels;

public class MoneyGoalViewModel : ViewModelBase
{
    private readonly MoneyGoal _moneyGoal;

    public MoneyGoalViewModel(MoneyGoal moneyGoal)
    {
        _moneyGoal = moneyGoal;
    }

    public string Title => _moneyGoal.Title;

    public decimal Amount => _moneyGoal.Amount;
    
    public Status Status => _moneyGoal.State;
}