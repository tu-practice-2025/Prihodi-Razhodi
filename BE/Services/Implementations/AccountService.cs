using Microsoft.EntityFrameworkCore;
using SummerPracticeWebApi.DataAccess.Context;
using SummerPracticeWebApi.Dtos;
using SummerPracticeWebApi.Mappers;
using SummerPracticeWebApi.Services.Interfaces;

namespace SummerPracticeWebApi.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IncomeExpensesContext _context;

        public AccountService (IncomeExpensesContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AccountDto>> GetAccountsByUserId(uint userId)
        {
            var accounts = await _context.Accounts
            .Where(account => account.UserId == userId)
            .ToListAsync();

            return accounts.Select(AccountMapper.MapToDto);
        }
    }
}
