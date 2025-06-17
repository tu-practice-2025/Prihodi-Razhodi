using SummerPracticeWebApi.Dtos;
using SummerPracticeWebApi.Dtos.Budget;

namespace SummerPracticeWebApi.Services.Interfaces
{
    public interface IBudgetService
    {
        Task AddBudgetAsync(PlanningBudgetDto dto);
        Task<IEnumerable<BudgetDto>> GetBudgetsByUserId(uint userId);
        Task<bool> UpdateBudgetAsync(BudgetDto dto);
        Task<bool> DeleteBudgetAsync(uint id);
        Task<IEnumerable<BudgetDto>> GetBudgetsByUserIdMonthAndYear(uint userId, int month, int year);
        Task<BudgetDto?> GetUserCategoryBudgetAsync(uint userId, string categoryCode, byte month, uint year);
    }
}
   