using SummerPracticeWebApi.Dtos;

namespace SummerPracticeWebApi.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategories();
        Task<List<CategorySpendingDto>> GetUserSpendingsByCategoryAsync(uint userId, byte month, uint year);
        Task<List<ExpenseTransactionDto>> GetLatestExpensesAsync(uint userId, byte month, uint year);
    }
}