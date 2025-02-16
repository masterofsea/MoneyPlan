using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Windows.Input;
using MoneyPlan.Models;
using MoneyPlan.Repositories;
using MoneyPlan.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Reactive.Concurrency;

namespace MoneyPlan.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public const int AccountId = 1;
    private readonly AccountService _accountService;
    public Interaction<CreateMoneyGoalViewModel, MoneyGoalViewModel?> ShowDialog { get; }
    
    public ICommand CreateMoneyGoalCommand { get; }


    private async void LoadAccount()
    {
        await _accountService.AddAccount(new Account
        {
            CashMoney = 100,
            CardMoney = 1800,
            SavingMoney = 300,
            EndOfPeriodDate = DateTime.Now.AddDays(25),
        });
            
        Account = await _accountService.GetAccount(AccountId);
            
        RecalculateBalanceAfterSpending();
    }
    public MainWindowViewModel()
    {
        _accountService = new AccountService(new AccountRepository(new ApplicationContext()));

        RxApp.MainThreadScheduler.Schedule(LoadAccount);
        

        ShowDialog = new Interaction<CreateMoneyGoalViewModel, MoneyGoalViewModel?>();

        CreateMoneyGoalCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var goal = new CreateMoneyGoalViewModel();

            var result = await ShowDialog.Handle(goal);

            if (result is not null)
            {
                MoneyGoals.Add(result);
            }
        });



        // AddGoalCommand = ReactiveCommand.Create(() =>
        // {
        //     MoneyGoals.Add(new MoneyGoal
        //     {
        //         Title = NewGoalTitle!,
        //         Amount = NewGoalAmount,
        //         State = Status.Waiting,
        //         Period = Period.Once,
        //         Significance = Significance.Required,
        //     });
        //
        //     RecalculateBalanceAfterSpending();
        // });


        // Task.Run(async () =>
        // {
        //     var jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        //
        //     var c = CultureInfo.CurrentCulture;
        //     var r = new RegionInfo(c.LCID);
        //     string name = r.Name;
        //     
        //     using var httpClient = new HttpClient();
        //     using var response = await httpClient.GetAsync($"https://date.nager.at/api/v3/publicholidays/{DateTime.Now.Year}/DE");
        //     if (response.IsSuccessStatusCode)
        //     {
        //         await using var jsonStream = await response.Content.ReadAsStreamAsync();
        //         var publicHolidays = JsonSerializer.Deserialize<PublicHoliday[]>(jsonStream, jsonSerializerOptions);
        //
        //         if (publicHolidays != null)
        //         {
        //             PublicHolidays.AddRange(publicHolidays);
        //         }
        //     }
        // });
    }

    public ObservableCollection<PublicHoliday> PublicHolidays { get; set; } = [];
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

    private void RecalculateBalanceAfterSpending()
    {
        // var dailyCons = MoneyGoals.Where(g => g.Period is Period.Daily).Sum(g => g.Amount *
        //     (decimal)(Account.EndOfPeriodDate - DateTime.Now).TotalDays);
        // var onceCons = MoneyGoals.Where(g => g.State is Status.Waiting).Sum(g => g.Amount);
        //
        // BalanceAfterSpending = Account.CurrentMoneyPool - dailyCons - onceCons;
    }
}