using System;
using System.Threading.Tasks;
using MoneyPlan.Models;

namespace MoneyPlan.Services;

public interface IAccountService
{
    Task<Account> GetAccount(int accountId);
    
    Task UpdateAccount(Account account);
    
    Task AddAccount(Account account);
}