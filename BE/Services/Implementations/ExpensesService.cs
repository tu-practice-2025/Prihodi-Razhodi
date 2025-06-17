using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SummerPracticeWebApi.DataAccess.Context;
using SummerPracticeWebApi.Dtos;
using SummerPracticeWebApi.Mappers;
using SummerPracticeWebApi.Models;
using SummerPracticeWebApi.Services.Interfaces;

namespace SummerPracticeWebApi.Services.Implementations
{
    public class ExpensesService : IExpensesService
    {
        private readonly IncomeExpensesContext _context;

        public ExpensesService(IncomeExpensesContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategories()
        {
            var categories = await _context.Categories.ToListAsync();
            return categories.Select(CategoryMapper.MapToDto);
        }

        public async Task<List<CategorySpendingDto>> GetUserSpendingsByCategoryAsync(uint userId, byte month, uint year)
        {
            var spendings = await _context.Operations
                .Where(op =>
                    op.IsExpense &&
                    op.Acc.UserId == userId &&
                    op.DateTime.Month == month &&
                    op.DateTime.Year == year
                )
                .GroupBy(op => op.CategoryCodeNavigation.Description ?? "Uncategorized")
                .Select(group => new CategorySpendingDto
                {
                    Category = group.Key,
                    Total = group.Sum(op => op.AmountLcy)
                })
                .ToListAsync();

            return spendings;
        }

        public async Task<List<ExpenseTransactionDto>> GetLatestExpensesAsync(uint userId, byte month, uint year, int skip, int take)
        {
            return await _context.Operations
                .Include(op => op.CategoryCodeNavigation)
                .Include(op => op.Acc)
                .Where(op =>
                    op.IsExpense &&
                    op.Acc.UserId == userId &&
                    op.DateTime.Month == month &&
                    op.DateTime.Year == year)
                .OrderByDescending(op => op.DateTime)
                .Skip(skip)
                .Take(take)
                .Select(op => new ExpenseTransactionDto
                {
                    Date = op.DateTime,
                    Category = op.CategoryCodeNavigation.Description ?? "Uncategorized",
                    Amount = op.AmountLcy
                })
                .ToListAsync();
        }
    }
}