using System.Threading.Tasks;
using MoneyPlan.Models;

namespace MoneyPlan.Repositories;

public interface IAccountRepository
{
    public Task<AccountDto> GetAccount(int accountId);
    
    public Task AddAccount(AccountDto account);
}