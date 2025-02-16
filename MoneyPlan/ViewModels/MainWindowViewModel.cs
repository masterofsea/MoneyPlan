using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MoneyPlan.Models;
using MoneyPlan.Repositories;
using MoneyPlan.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace MoneyPlan.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public const int AccountId = 1;
    public MainWindowViewModel()
    {
        var accountService = new AccountService(new AccountRepository(new ApplicationContext()));

        

        Task.Run(async () =>
        {
            await accountService.AddAccount(new Account
            {
                CashMoney = 100,
                CardMoney = 1800,
                SavingMoney = 300,
                EndOfPeriodDate = DateTime.Now.AddDays(25),
            });
            
            Account = await accountService.GetAccount(AccountId);
            
            RecalculateBalanceAfterSpending();
        });
        
         

        AddGoalCommand = ReactiveCommand.Create(() =>
        {
            MoneyGoals.Add(new MoneyGoal
            {
                Title = NewGoalTitle!,
                Amount = NewGoalAmount,
                State = Status.Waiting,
                Period = Period.Once,
                Significance = Significance.Required,
            });

            RecalculateBalanceAfterSpending();
        });
        
        
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
    public ObservableCollection<MoneyGoal> MoneyGoals { get; set; } =
    [
        new()
        {
            Title = "Colleague birthday",
            Amount = 50,
            State = Status.Completed
        },

        new()
        {
            Title = "Deutsch ticket",
            Amount = 244,
            State = Status.Waiting
        },
        new()
        {
            Title = "Food",
            Amount = 19,
            Period = Period.Daily,
            State = Status.Continuation,
        }
    ];
    
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

    public string NewGoalTitle { get; set; }

    public decimal NewGoalAmount { get; set; }

    public ICommand AddGoalCommand { get; }

    private void RecalculateBalanceAfterSpending()
    {
        var dailyCons = MoneyGoals.Where(g => g.Period is Period.Daily).Sum(g => g.Amount *
            (decimal)(Account.EndOfPeriodDate - DateTime.Now).TotalDays);
        var onceCons = MoneyGoals.Where(g => g.State is Status.Waiting).Sum(g => g.Amount);

        BalanceAfterSpending = Account.CurrentMoneyPool - dailyCons - onceCons;
    }
}