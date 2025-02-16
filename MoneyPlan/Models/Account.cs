using System;

namespace MoneyPlan.Models;

public class Account
{
    public int? Id { get; init; }
    public required decimal SavingMoney { get; init; }
    public required decimal CashMoney { get; init; }
    public required decimal CardMoney { get; init; }
    public decimal TotalMoney => SavingMoney + CurrentMoneyPool;
    public decimal CurrentMoneyPool => CashMoney + CardMoney;
    public required DateTime EndOfPeriodDate { get; init; }
}