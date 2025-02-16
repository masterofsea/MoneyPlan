using Avalonia.ReactiveUI;
using MoneyPlan.ViewModels;
using ReactiveUI;
using System;

namespace MoneyPlan.Views;

public partial class CreateMoneyGoalWindow : ReactiveWindow<CreateMoneyGoalViewModel>
{
    public CreateMoneyGoalWindow()
    {
        InitializeComponent();
        
        this.WhenActivated(action => action(ViewModel!.AddNewGoalCommand.Subscribe(Close)));

    }
}