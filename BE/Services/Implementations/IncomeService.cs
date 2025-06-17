using Microsoft.EntityFrameworkCore;
using SummerPracticeWebApi.DataAccess.Context;
using SummerPracticeWebApi.Dtos;
using SummerPracticeWebApi.Services.Interfaces;

namespace SummerPracticeWebApi.Services.Implementations
{
    public class IncomeService : IIncomeService
    {
        private readonly IncomeExpensesContext _context;

        public IncomeService(IncomeExpensesContext context)
        {
            _context = context;
        }

        public async Task<List<DailyIncomeDto>> GetUserIncomeByDayAsync(uint userId, byte month, uint year)
        {
            return await _context.Operations
                .Where(op => !op.IsExpense &&
                             op.Acc.UserId == userId &&
                             op.DateTime.Month == month &&
                             op.DateTime.Year == year)
                .GroupBy(op => op.DateTime.Date)
                .Select(group => new DailyIncomeDto
                {
                    Date = group.Key,
                    Total = group.Sum(op => op.AmountLcy)
                })
                .OrderBy(x => x.Date)
                .ToListAsync();
        }

        public async Task<List<IncomeTransactionDto>> GetLatestIncomesAsync(uint userId, byte month, uint year, int skip = 0, int take = 10)
        {
            return await _context.Operations
                .Where(op => !op.IsExpense &&
                             op.Acc.UserId == userId &&
                             op.DateTime.Month == month &&
                             op.DateTime.Year == year)
                .OrderByDescending(op => op.DateTime)
                .Skip(skip)
                .Take(take)
                .Select(op => new IncomeTransactionDto
                {
                    Date = op.DateTime,
                    Source = op.Description ?? "No Description",
                    Amount = op.AmountLcy
                })
                .ToListAsync();
        }
    }
}