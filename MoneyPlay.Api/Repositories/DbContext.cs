using Microsoft.EntityFrameworkCore;
using MoneyPlan.Domain.Models;

namespace MoneyPlay.Api.Repositories;

public sealed class ApplicationContext : DbContext
{
    public DbSet<MoneyGoal> MoneyGoals => Set<MoneyGoal>();

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MoneyGoal>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.Property(e => e.Amount)
                .HasConversion<double>();
        });
    }
}