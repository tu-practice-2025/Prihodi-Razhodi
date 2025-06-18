using Microsoft.AspNetCore.Mvc;
using SummerPracticeWebApi.Services.Interfaces;
using SummerPracticeWebApi.Dtos;
using SummerPracticeWebApi.Services.Implementations;

namespace SummerPracticeWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService _categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        [HttpGet("/{userId}/category-details")]
        public async Task<IActionResult> GetCategoryDetails(uint userId, [FromQuery] string code, [FromQuery] byte month, [FromQuery] uint year)
        {
            var result = await _categoriesService.GetCategoryDetailsAsync(userId, code, month, year);
            return Ok(result);
        }




    }
}
