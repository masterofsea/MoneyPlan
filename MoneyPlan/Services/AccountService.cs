// using System;
// using System.Threading.Tasks;
// using MoneyPlan.Models;
// using MoneyPlan.Repositories;
//
// namespace MoneyPlan.Services;
//
// public class AccountService(IAccountRepository accountRepository) : IAccountService
// {
//     public async Task<Account> GetAccount(int accountId)
//     {
//         var accountDto = await accountRepository.GetAccount(accountId);
//
//         return new Account
//         {
//             Id = accountDto.Id,
//             CashMoney = accountDto.CashMoney,
//             CardMoney = accountDto.CardMoney,
//             SavingMoney = accountDto.SavingMoney,
//             EndOfPeriodDate = accountDto.EndOfPeriodDate,
//         };
//     }
//
//     public Task UpdateAccount(Account account)
//     {
//         throw new NotImplementedException();
//     }
//
//     public async Task AddAccount(Account account)
//     {
//         var accountDto = new AccountDto
//         {
//             CashMoney = account.CashMoney,
//             CardMoney = account.CardMoney,
//             SavingMoney = account.SavingMoney,
//             EndOfPeriodDate = account.EndOfPeriodDate,
//         };
//         
//         await accountRepository.AddAccount(accountDto);
//     }
// }