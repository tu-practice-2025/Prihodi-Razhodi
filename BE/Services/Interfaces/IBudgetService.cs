using SummerPracticeWebApi.Dtos;
using SummerPracticeWebApi.Dtos.Budget;
using SummerPracticeWebApi.Models;

namespace SummerPracticeWebApi.Services.Interfaces
{
    public interface IBudgetService
    {
        Task<IEnumerable<BudgetDto>> GetBudgetsByUserIdMonthAndYear(uint userId, int month, int year);
        Task<BudgetDto?> GetUserCategoryBudgetAsync(uint userId, string categoryCode, byte month, uint year);
    }
}
   