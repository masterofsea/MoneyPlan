using Microsoft.EntityFrameworkCore;
using MoneyPlan.Models;

namespace MoneyPlan.Repositories;

public sealed class ApplicationContext : DbContext
{
    public DbSet<AccountDto> Accounts => Set<AccountDto>();
    
    public ApplicationContext()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=money_plan.db");
    }
}