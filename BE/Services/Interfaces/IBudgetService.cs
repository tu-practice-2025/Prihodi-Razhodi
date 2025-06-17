using SummerPracticeWebApi.Dtos.Budget;
using SummerPracticeWebApi.Models;

namespace SummerPracticeWebApi.Services.Interfaces
{
    public interface IBudgetService
    {
        Task AddBudgetAsync(PlanningBudgetDto dto);
        Task<IEnumerable<BudgetDto>> GetBudgetsByUserAsync(uint userId);
        Task<bool> UpdateBudgetAsync(BudgetDto dto);
        Task<bool> DeleteBudgetAsync(uint id);
    }
}