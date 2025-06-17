using Microsoft.EntityFrameworkCore;
using SummerPracticeWebApi.DataAccess.Context;
using SummerPracticeWebApi.Services.Interfaces;

namespace SummerPracticeWebApi.Services.Implementations
{
    public class SummaryService : ISummaryService
    {
        private readonly IncomeExpensesContext _context;

        public SummaryService (IncomeExpensesContext context)       
        {
            _context = context;
        }

        public async Task<decimal> getExpensesByMonthAndYear(uint userId, int month, int year)
        {
            decimal expenses = await _context.Operations
                .Where(operation => operation.Acc.UserId == userId &&
                    operation.IsExpense == true &&
                    operation.DateTime.Month == month &&
                    operation.DateTime.Year == year)
                .Include(operation => operation.Acc)
                .Include(operation => operation.CategoryCodeNavigation)
                .Include(operation => operation.Card)
                .SumAsync(operation => operation.AmountLcy);

            return expenses;
        }

        public async Task<decimal> getIncomeByMonthAndYear(uint userId, int month, int year)
        {
            decimal expenses = await _context.Operations
                .Where(operation => operation.Acc.UserId == userId &&
                    operation.IsExpense == false &&
                    operation.DateTime.Month == month &&
                    operation.DateTime.Year == year)
                .Include(operation => operation.Acc)
                .Include(operation => operation.CategoryCodeNavigation)
                .Include(operation => operation.Card)
                .SumAsync(operation => operation.AmountLcy);

            return expenses;
        }

        public async Task<decimal> getBalanceSummary(uint userId)
        {
            decimal balance = await _context.Accounts
                .Where(account => account.UserId == userId)
                .SumAsync(account => account.Balance);

            return balance;
        }
    }
}
