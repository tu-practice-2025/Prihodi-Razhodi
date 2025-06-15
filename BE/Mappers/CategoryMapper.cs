using SummerPracticeWebApi.Dtos;
using SummerPracticeWebApi.Models;

namespace SummerPracticeWebApi.Mappers
{
    public class CategoryMapper
    {
        public static CategoryDto MapToDto(Category Category)
        {
            return new CategoryDto
            {
                Code = Category.Code,
                Description = Category.Description
            };
        }
    }
}
