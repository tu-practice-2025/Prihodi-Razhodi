using SummerPracticeWebApi.Dtos.Budget;
using SummerPracticeWebApi.Models;

namespace SummerPracticeWebApi.Services.Interfaces
{
    public interface IBudgetService
    {
        Task AddBudgetAsync(PlanningBudgetDto dto);
        Task<List<BudgetDto>> GetBudgetsByUserAsync(uint userId);
        Task<bool> UpdateBudgetAsync(uint id, PlanningBudgetDto dto);
        Task<bool> DeleteBudgetAsync(uint id);
    }
}