using Microsoft.EntityFrameworkCore;

namespace MoneyPlan.Models;

[PrimaryKey(nameof(Id))]
public class MoneyGoalDto
{
    public int Id { get; init; }

    public required string Title { get; init; }

    public required decimal Amount { get; init; }

    public required Status State { get; init; }

    public required Period Period { get; init; }
    
    public required Significance Significance { get; init; }
}