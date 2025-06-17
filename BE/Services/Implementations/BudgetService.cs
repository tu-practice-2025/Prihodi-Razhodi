using SummerPracticeWebApi.Dtos.Budget;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using SummerPracticeWebApi.DataAccess.Context;
using SummerPracticeWebApi.Dtos;
using SummerPracticeWebApi.Mappers;
using SummerPracticeWebApi.Services.Interfaces;
using SummerPracticeWebApi.Models;

namespace SummerPracticeWebApi.Services.Implementations
{
    public class BudgetService : IBudgetService
    {
        private readonly IncomeExpensesContext _context;

        public BudgetService(IncomeExpensesContext context)
        {
            _context = context;
        }

        public async Task<BudgetDto?> GetUserCategoryBudgetAsync(uint userId, string categoryCode, byte month, uint year)
        {
            var budget = await _context.Budgets
                .Where(b => b.UserId == userId && b.CategoryCode == categoryCode && b.Month == month && b.Year == year)
                .FirstOrDefaultAsync();
            var budget = await _context.Budgets.FindAsync(dto.Id);
            if (budget == null) return false;

            budget.Amount = dto.Amount;
            budget.CategoryCode = dto.CategoryCode; // use code, not description
            budget.Month = dto.Month;
            budget.Year = dto.Year;
            budget.UserId = dto.UserId;

            if (budget == null) return null;

            return new BudgetDto { Amount = budget.Amount };
        }

        public async Task<IEnumerable<BudgetDto>> GetBudgetsByUserIdMonthAndYear(uint userId, int month, int year)
        {
            var budgets = await _context.Budgets
                .Include(b => b.CategoryCodeNavigation)
                .Where(b => b.UserId == userId && 
                            b.Month == month &&
                            b.Year == year)
                .ToListAsync();

            return budgets.Select(BudgetMapper.MapToBudgetDto);
        }
    }
}