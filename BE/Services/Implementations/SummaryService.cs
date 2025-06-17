using Microsoft.EntityFrameworkCore;
using SummerPracticeWebApi.DataAccess.Context;
using SummerPracticeWebApi.Mappers;
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

        public async Task<decimal> GetExpensesByMonthAndYear(uint userId, int month, int year)
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

        public async Task<decimal> GetIncomeByMonthAndYear(uint userId, int month, int year)
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

        public async Task<decimal> GetBalanceSummary(uint userId)
        {
            decimal balance = await _context.Accounts
                .Where(account => account.UserId == userId)
                .SumAsync(account => account.Balance);

            return balance;
        }

        public async Task<Dictionary<string, decimal>> GetExpensesCategorised(uint userId, int month, int year)
        {
            var totals = await _context.Operations
                .Where(o => o.Acc.UserId == userId
                         && o.IsExpense == true
                         && o.DateTime.Month == month
                         && o.DateTime.Year == year)
                .GroupBy(o => o.CategoryCode ?? "OTHR")
                .Select(g => new {
                    Category = g.Key,
                    Total = g.Sum(o => o.AmountLcy)
                })
                .ToDictionaryAsync(x => x.Category, x => x.Total);

            return totals;
        }
    }
}
