namespace MoneyPlan.Domain.Models;

public record MoneyGoal
{
    public Guid? Id { get; init; }

    public required Guid AccountId { get; init; }
    
    public required string Title { get; init; }

    public required decimal Amount { get; init; }

    public required Status State { get; init; }

    public Period Period { get; init; } = Period.Once;

    public Significance Significance { get; init; } = Significance.Required;
}