namespace MoneyPlan.Models;

public record MoneyGoal
{
    public int? Id { get; init; }

    public required string Title { get; init; }

    public required decimal Amount { get; init; }

    public required Status State { get; init; }

    public Period Period { get; init; } = Period.Once;
    
    public Significance Significance { get; init; } = Significance.Required;
}

public enum Status
{
    Continuation,
    Cancelled,
    Waiting,
    Completed,
}

public enum Significance
{
    Required,
    Wishful,
}

public enum Period
{
    Daily,
    Monthly,
    Once,
}