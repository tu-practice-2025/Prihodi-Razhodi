using Microsoft.EntityFrameworkCore;
using SummerPracticeWebApi.DataAccess.Context;
using SummerPracticeWebApi.Dtos.Budget;
using SummerPracticeWebApi.Services.Interfaces;

namespace SummerPracticeWebApi.Services.Implementations
{
    public class BudgetService : IBudgetService
    {
        private readonly IncomeExpensesContext _context;

        public BudgetService(IncomeExpensesContext context)
        {
            _context = context;
        }

        public async Task<List<BudgetDto>> GetUserBudgetsAsync(uint userId)
        {
            return await _context.Budgets
                .Where(b => b.UserId == userId)
                .Include(b => b.CategoryCodeNavigation)
                .Select(b => new BudgetDto
                {
                    Id = b.Id,
                    Amount = b.Amount,
                    Currency = b.Currency,
                    Month = b.Month,
                    Year = b.Year,
                    CategoryDescription = b.CategoryCodeNavigation.Description,
                    UserId = b.UserId,
                    CategoryCode = b.CategoryCode
                })
                .ToListAsync();
        }

        public async Task<BudgetDto?> GetUserCategoryBudgetAsync(uint userId, string categoryCode, byte month, uint year)
        {
            var budget = await _context.Budgets
                .Include(b => b.CategoryCodeNavigation)
                .FirstOrDefaultAsync(b =>
                    b.UserId == userId &&
                    b.CategoryCode == categoryCode &&
                    b.Month == month &&
                    b.Year == year);

            if (budget == null) return null;

            return new BudgetDto
            {
                Id = budget.Id,
                Amount = budget.Amount,
                Currency = budget.Currency,
                Month = budget.Month,
                Year = budget.Year,
                CategoryDescription = budget.CategoryCodeNavigation?.Description ?? string.Empty,
                UserId = budget.UserId,
                CategoryCode = budget.CategoryCode ?? string.Empty
            };
        }

        public async Task<BudgetDto> CreateBudgetAsync(BudgetDto budgetDto)
        {
            var entity = new Models.Budget
            {
                Amount = budgetDto.Amount,
                Currency = budgetDto.Currency,
                CategoryCode = budgetDto.CategoryCode,
                Month = budgetDto.Month,
                Year = budgetDto.Year,
                UserId = budgetDto.UserId
            };

            _context.Budgets.Add(entity);
            await _context.SaveChangesAsync();

            budgetDto.Id = entity.Id; // Update DTO with generated Id

            return budgetDto;
        }

        public async Task<bool> UpdateBudgetAsync(BudgetDto budgetDto)
        {
            var entity = await _context.Budgets.FindAsync(budgetDto.Id);
            if (entity == null)
                return false;

            entity.Amount = budgetDto.Amount;
            entity.Currency = budgetDto.Currency;
            entity.CategoryCode = budgetDto.CategoryCode;
            entity.Month = budgetDto.Month;
            entity.Year = budgetDto.Year;
            entity.UserId = budgetDto.UserId;

            await _context.SaveChangesAsync();

            return true;
        }

    
        public async Task<bool> DeleteBudgetAsync(uint id)
        {
            var budget = await _context.Budgets.FindAsync(id);
            if (budget == null)
                return false;

            _context.Budgets.Remove(budget);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
