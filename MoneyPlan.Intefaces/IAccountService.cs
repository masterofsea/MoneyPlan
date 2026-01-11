using MoneyPlan.Domain.Models;

namespace MoneyPlan.Intefaces;

public interface IAccountService
{
    Task<Account> GetAccount(int accountId);
    
    Task UpdateAccount(Account account);
    
    Task AddAccount(Account account);
}