using System;
using Microsoft.EntityFrameworkCore;

namespace MoneyPlan.Models;

[PrimaryKey(nameof(Id))]
public record AccountDto
{
    public int Id { get; init; }

    public required decimal SavingMoney { get; init; }

    public required decimal CashMoney { get; init; }
    
    public required decimal CardMoney { get; init; }
    
    public required DateTime EndOfPeriodDate { get; init; }
}