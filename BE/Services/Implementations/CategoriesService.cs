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
            var operations = await _context.Operations
                .Include(op => op.Acc)
                .Where(op =>
                    op.IsExpense &&
                    op.Acc.UserId == userId &&
                    op.CategoryCode == categoryCode &&
                    op.DateTime.Month == month &&
                    op.DateTime.Year == year)
                .OrderByDescending(op => op.DateTime)
                .ToListAsync();

            var dto = new CategoryDetailsDto
            {
                TotalSpent = operations.Sum(op => op.AmountLcy),
                Transactions = operations.Select(op => new CategoryTransactionDto
                {
                    Date = op.DateTime,
                    Description = op.Description ?? "No Description",
                    Amount = op.AmountLcy
                }).ToList()
            };

            return dto;
        }

    }
}