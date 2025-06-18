using Microsoft.AspNetCore.Mvc;
using SummerPracticeWebApi.Dtos.Budget;
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

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserBudgets(uint userId)
        {
            var result = await _budgetService.GetUserBudgetsAsync(userId);
            return Ok(result);
        }

        [HttpGet("{userId}/category")]
        public async Task<IActionResult> GetUserCategoryBudget(uint userId, [FromQuery] string code, [FromQuery] byte month, [FromQuery] uint year)
        {
            var result = await _budgetService.GetUserCategoryBudget(userId, code, month, year);
            if (result == null) return NotFound("No budget set for this category.");
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBudget([FromBody] BudgetDto budgetDto)
        {
            if (budgetDto == null)
                return BadRequest();

            var createdBudget = await _budgetService.CreateBudgetAsync(budgetDto);
            return CreatedAtAction(nameof(GetUserBudgets), new { userId = createdBudget.UserId }, createdBudget);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBudget([FromBody] BudgetDto budgetDto)
        {
            if (budgetDto == null || budgetDto.Id == 0)
                return BadRequest();

            var updated = await _budgetService.UpdateBudgetAsync(budgetDto);

            if (!updated)
                return NotFound();

            return NoContent();
        }

        // Changed parameter to uint to match the entity key type
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBudget(uint id)
        {
            var success = await _budgetService.DeleteBudgetAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
