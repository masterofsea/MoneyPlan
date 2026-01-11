using System;
using System.Reactive;
using MoneyPlan.Domain.Models;
using MoneyPlan.Intefaces;
using ReactiveUI;

namespace MoneyPlan.ViewModels;

public class CreateMoneyGoalViewModel : ViewModelBase
{
    private readonly IGoalsService _goalsService;

    public CreateMoneyGoalViewModel(IGoalsService goalsService)
    {
        _goalsService = goalsService;

        AddNewGoalCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var t = new MoneyGoal
            {
                Title = NewGoalTitle,
                AccountId = Guid.Empty,
                Amount = NewGoalAmount,
                State = Status.Waiting,
                Period = Period.Once,
                Significance = Significance.Required,
            };

            await _goalsService.AddGoal(t);

            return new MoneyGoalViewModel(t);
        });
    }

    public ReactiveCommand<Unit, MoneyGoalViewModel> AddNewGoalCommand { get; }

    public string NewGoalTitle { get; set; } = string.Empty;

    public decimal NewGoalAmount { get; set; } = 0;
}