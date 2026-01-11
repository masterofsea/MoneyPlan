using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Windows.Input;
using MoneyPlan.Domain.Models;
using MoneyPlan.Intefaces;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace MoneyPlan.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public Interaction<CreateMoneyGoalViewModel, MoneyGoalViewModel?> ShowDialog { get; }
    
    public ICommand CreateMoneyGoalCommand { get; }
    
    public MainWindowViewModel(IGoalsService goalsService)
    {
        //RxApp.MainThreadScheduler.Schedule(LoadAccount);
        
        ShowDialog = new Interaction<CreateMoneyGoalViewModel, MoneyGoalViewModel?>();

        CreateMoneyGoalCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var goal = new CreateMoneyGoalViewModel(goalsService);

            var result = await ShowDialog.Handle(goal);

            if (result is not null)
            {
                MoneyGoals.Add(result);
            }
        });
    }

    public ObservableCollection<MoneyGoalViewModel> MoneyGoals { get; set; } = [];
    
    public ObservableCollection<MoneySource> MoneySources { get; set; } =
    [
        new GuaranteedMoneySource(new GuaranteedWorkDayTimeExpectation
        {
            GuaranteedLatestDay = 25
        })
        {
            Title = "Salary",
            Amount = 4700,
        },
        
        new PeriodicalMoneySource(new PeriodOfTimeExpectation())
        {
            Title = "Rent",
            Amount = 1500,
        }
    ];

    [Reactive] public Account Account { get; private set; }

    [Reactive] public decimal BalanceAfterSpending { get; private set; }
}