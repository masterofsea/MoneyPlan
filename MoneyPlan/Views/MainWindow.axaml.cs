using System.Threading.Tasks;
using Avalonia.ReactiveUI;
using MoneyPlan.ViewModels;
using ReactiveUI;

namespace MoneyPlan.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
        
        this.WhenActivated(action => action(ViewModel!.ShowDialog.RegisterHandler(DoShowDialogAsync)));
    }
    
    private async Task DoShowDialogAsync(IInteractionContext<CreateMoneyGoalViewModel,
        MoneyGoalViewModel?> interaction)
    {
        var dialog = new CreateMoneyGoalWindow
        {
            DataContext = interaction.Input
        };

        var result = await dialog.ShowDialog<MoneyGoalViewModel?>(this);
        interaction.SetOutput(result);
    }
}