using Microsoft.AspNetCore.Mvc;
using SummerPracticeWebApi.Services.Interfaces;
using SummerPracticeWebApi.Dtos;

namespace SummerPracticeWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var result = await _categoryService.GetAllCategories();
            return Ok(result);
        }

        // GET: /api/Category/{userId}/spending
        [HttpGet("{userId}/spending")]
        public async Task<IActionResult> GetSpendingByCategory(uint userId, [FromQuery] byte month, [FromQuery] uint year)
        {
            var result = await _categoryService.GetUserSpendingsByCategoryAsync(userId, month, year);
            return Ok(result);
        }

        [HttpGet("{userId}/latest")]
        public async Task<IActionResult> GetLatestExpenses(uint userId, [FromQuery] byte month, [FromQuery] uint year)
        {
            var result = await _categoryService.GetLatestExpensesAsync(userId, month, year);
            return Ok(result);
        }
    }
}
