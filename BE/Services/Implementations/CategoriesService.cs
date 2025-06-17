using Microsoft.EntityFrameworkCore;
using SummerPracticeWebApi.DataAccess.Context;
using SummerPracticeWebApi.Dtos;
using SummerPracticeWebApi.Services.Interfaces;

namespace SummerPracticeWebApi.Services.Implementations
{
    public class CategoriesService : ICategoriesService
    {
        private readonly IncomeExpensesContext _context;

        public CategoriesService(IncomeExpensesContext context)
        {
            _context = context;
        }

        public async Task<CategoryDetailsDto> GetCategoryDetailsAsync(uint userId, string categoryCode, byte month, uint year)
        {
            var startDate = new DateTime((int)year, month, 1);
            var endDate = startDate.AddMonths(1);

            // Try to get the category name regardless of transactions
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Code == categoryCode);

            // Get operations (may be empty)
            var operations = await _context.Operations
                .Include(op => op.Acc)
                .Where(op =>
                    op.IsExpense &&
                    op.Acc.UserId == userId &&
                    op.CategoryCode == categoryCode &&
                    op.DateTime >= startDate &&
                    op.DateTime < endDate)
                .OrderByDescending(op => op.DateTime)
                .ToListAsync();

            return new CategoryDetailsDto
            {
                CategoryDescription = category?.Description ?? categoryCode,
                TotalSpent = operations.Sum(op => op.AmountLcy),
                Transactions = operations.Select(op => new CategoryTransactionDto
                {
                    Date = op.DateTime,
                    Description = op.Description ?? "No Description",
                    Amount = op.AmountLcy
                }).ToList()
            };
        }

    }
}
