using Microsoft.EntityFrameworkCore;
using MoneyPlan.Models;

namespace MoneyPlan.Repositories;

public sealed class ApplicationContext : DbContext
{
    public DbSet<AccountDto> Accounts => Set<AccountDto>();
    
    public ApplicationContext()
    {
        // Database.GetDbConnection().Close();
        // Dispose();

        Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=mydatabase.db;Pooling=False");
        //optionsBuilder.UseSqlite("Data Source=:memory:");
    }
}