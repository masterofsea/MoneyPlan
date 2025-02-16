using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MoneyPlan.Models;

namespace MoneyPlan.Repositories;

public class AccountRepository(ApplicationContext context) : IAccountRepository
{
    public async Task<AccountDto> GetAccount(int accountId)
    {
        return await context.Accounts.FirstAsync(account => account.Id == accountId);
    }

    public async Task AddAccount(AccountDto account)
    {
        context.Accounts.Add(account);
        await context.SaveChangesAsync();
    }
}