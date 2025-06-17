using SummerPracticeWebApi.Dtos;
using SummerPracticeWebApi.Dtos.Budget;

namespace SummerPracticeWebApi.Services.Interfaces
{
    public interface IBudgetService
    {
        Task<BudgetDto?> GetUserCategoryBudgetAsync(uint userId, string categoryCode, byte month, uint year);
    }
}
   