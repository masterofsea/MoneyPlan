namespace MoneyPlan.Domain.Models;

public record PublicHoliday
{
    public DateTime Date { get; set; }
    public required string LocalName { get; set; }
    public required string Name { get; set; }
    public required string CountryCode { get; set; }
    public bool Fixed { get; set; }
    public bool Global { get; set; }
    public required string[] Counties { get; set; }
    public int? LaunchYear { get; set; }
    public required string[] Types { get; set; }
}