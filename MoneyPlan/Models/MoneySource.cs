namespace MoneyPlan.Models;

public abstract class MoneySource(ITimeExpectation expectation)
{
    public required string Title { get; init; }
    
    public required decimal Amount { get; init; }
    
    public ITimeExpectation Expectation {get; } = expectation;
}

public class GuaranteedMoneySource(GuaranteedWorkDayTimeExpectation expectation) : MoneySource(expectation);

public class PeriodicalMoneySource(PeriodOfTimeExpectation expectation) : MoneySource(expectation)
{
    
}