using Microsoft.EntityFrameworkCore;

namespace MoneyPlan.Models;

[PrimaryKey(nameof(Id))]
public record PersonDto
{
    public required int Id { get; init; }
    public required string FirstName { get; init; }

    public required string LastName { get; init; }
}