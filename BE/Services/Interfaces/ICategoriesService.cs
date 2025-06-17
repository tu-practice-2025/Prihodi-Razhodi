using SummerPracticeWebApi.Dtos;

namespace SummerPracticeWebApi.Services.Interfaces
{
    public interface ICategoriesService
    {
        Task<CategoryDetailsDto> GetCategoryDetailsAsync(uint userId, string categoryCode, byte month, uint year);
    }
}
   