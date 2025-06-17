using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SummerPracticeWebApi.DataAccess.Context;
using SummerPracticeWebApi.Dtos.Budget;
using SummerPracticeWebApi.Mappers;
using SummerPracticeWebApi.Models;
using SummerPracticeWebApi.Services.Interfaces;

namespace SummerPracticeWebApi.Services.Implementations
{
    public class PlanningService : IPlanningService
    {
        private readonly IncomeExpensesContext _context;

        public PlanningService(IncomeExpensesContext context)
        {
            _context = context;
        }

        public async Task AddBudgetAsync(PlanningBudgetDto dto)
        {
            var budget = BudgetMapper.MapPlanningBudgetDtoToModel(dto);
            _context.Budgets.Add(budget);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BudgetDto>> GetBudgetsByUserAsync(uint userId)
        {
            var budgets = await _context.Budgets
                .Include(b => b.CategoryCodeNavigation)
                .Where(b => b.UserId == userId)
                .ToListAsync();

            return budgets.Select(BudgetMapper.MapToBudgetDto);
        }

        public async Task<bool> UpdateBudgetAsync(BudgetDto dto)
        {
            var budget = await _context.Budgets.FindAsync(dto.Id);
            var category = await _context.Categories
                .FirstAsync(category => category.Description == dto.CategoryDescription);
            if (budget == null) return false;

            budget.Amount = dto.Amount;
            budget.CategoryCode = category.Code;
            budget.Month = dto.Month;
            budget.Year = dto.Year;
            budget.UserId = dto.UserId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteBudgetAsync(uint id)
        {
            var budget = await _context.Budgets.FindAsync(id);
            if (budget == null) return false;

            _context.Budgets.Remove(budget);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
