namespace MoneyPlan.Domain.Models;

public record Account
{
    public required Guid Id { get; init; }
    
    public required string Name { get; init; }
    
    public required decimal TotalBalance { get; init; }
    
    public List<MoneyGoal> Goals { get; init; } = [];
}