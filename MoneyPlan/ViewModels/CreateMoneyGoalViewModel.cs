using System.Reactive;
using MoneyPlan.Models;
using ReactiveUI;

namespace MoneyPlan.ViewModels;

public class CreateMoneyGoalViewModel : ViewModelBase
{
    public CreateMoneyGoalViewModel()
    {
        AddNewGoalCommand = ReactiveCommand.Create(() => new MoneyGoalViewModel(new MoneyGoal
        {
            Title = NewGoalTitle,
            Amount = NewGoalAmount,
            State = Status.Cancelled,
            Period = Period.Daily,
            Significance = Significance.Required,
        }));
    }

    public ReactiveCommand<Unit, MoneyGoalViewModel> AddNewGoalCommand { get; }


    public string NewGoalTitle { get; set; } = string.Empty;

    public decimal NewGoalAmount { get; set; } = 0;
}