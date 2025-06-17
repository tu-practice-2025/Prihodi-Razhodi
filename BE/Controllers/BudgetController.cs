using Microsoft.AspNetCore.Mvc;
using SummerPracticeWebApi.Services.Interfaces;

namespace SummerPracticeWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BudgetController : ControllerBase
    {
        private readonly IBudgetService _budgetService;

        public BudgetController(IBudgetService budgetService)
        {
            _budgetService = budgetService;
        }

        [HttpGet("{userId}/category")]
        public async Task<IActionResult> GetUserCategoryBudget(uint userId, [FromQuery] string code, [FromQuery] byte month, [FromQuery] uint year)
        {
            var result = await _budgetService.GetUserCategoryBudgetAsync(userId, code, month, year);
            if (result == null) return NotFound("No budget set for this category.");
            return Ok(result);
        }

    }
}
