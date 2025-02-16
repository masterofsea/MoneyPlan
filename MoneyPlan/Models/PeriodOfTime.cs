using System;

namespace MoneyPlan.Models;

public interface ITimeExpectation
{
    public DateTime NearestExpectedDate();
}

public class PeriodOfTimeExpectation : ITimeExpectation
{
    public DateTime Start { get; init; }

    public DateTime End { get; init; }

    public DateTime NearestExpectedDate() => Start;
}

public class GuaranteedWorkDayTimeExpectation : ITimeExpectation
{
    public required int GuaranteedLatestDay { get; init; }
    
    public DateTime NearestExpectedDate()
    {
        var currentDateTime = DateTime.Now;

        return DaysUntilNextSalary(currentDateTime);
    }

    private DateTime DaysUntilNextSalary(DateTime currentDate)
    {
        var targetDate = new DateTime(currentDate.Year, currentDate.Month, GuaranteedLatestDay);

        if (currentDate >= targetDate)
        {
            targetDate = targetDate.AddMonths(1);
        }

        var nextSalaryDate = GetNearestWorkday(targetDate);

        return nextSalaryDate;
    }
    
    private static DateTime GetNearestWorkday(DateTime inputDate)
    {
        if (inputDate.DayOfWeek is >= DayOfWeek.Monday and <= DayOfWeek.Friday)
        {
            return inputDate;
        }

        if (inputDate.DayOfWeek == DayOfWeek.Saturday)
        {
            return inputDate.AddDays(-1);
        }

        if (inputDate.DayOfWeek == DayOfWeek.Sunday)
        {
            return inputDate.AddDays(-2);
        }

        throw new InvalidOperationException("Invalid date");
    }
}

public abstract class ConditionalTimeExpectation : ITimeExpectation
{
    public DateTime NearestExpectedDate()
    {
        throw new NotImplementedException();
    }
}