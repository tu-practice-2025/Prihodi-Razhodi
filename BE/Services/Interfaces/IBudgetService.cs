using SummerPracticeWebApi.Dtos;
using SummerPracticeWebApi.Dtos.Budget;

namespace SummerPracticeWebApi.Services.Interfaces
{
    public interface IBudgetService
    {
        Task<List<BudgetDto>> GetUserBudgetsAsync(uint userId, byte? month = null, uint? year = null);
        Task<BudgetDto?> GetUserCategoryBudgetAsync(uint userId, string categoryCode, byte month, uint year);

        Task<BudgetDto> CreateBudgetAsync(BudgetDto budgetDto);
        Task<bool> UpdateBudgetAsync(BudgetDto budgetDto);
        Task<bool> DeleteBudgetAsync(uint id);
        Task<IEnumerable<BudgetDto>> GetBudgetsByUserIdMonthAndYear(uint userId, int month, int year);
        Task<BudgetDto> GetUserCategoryBudget(uint userId, string categoryCode, byte month, uint year);

    }
}
