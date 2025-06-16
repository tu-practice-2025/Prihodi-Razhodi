using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SummerPracticeWebApi.DataAccess.Context;
using SummerPracticeWebApi.Dtos.Budget;
using SummerPracticeWebApi.Enums;
using SummerPracticeWebApi.Models;
using SummerPracticeWebApi.Services.Interfaces;

namespace SummerPracticeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetController : ControllerBase
    {
        private readonly IBudgetService _budgetService;

        public BudgetController(IBudgetService budgetService)
        {
            _budgetService = budgetService;
        }

        [HttpPost]
        public async Task<IActionResult> AddBudget([FromBody] PlanningBudgetDto dto)
        {
            await _budgetService.AddBudgetAsync(dto);
            return Ok();
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetBudgetsByUser(uint userId)
        {
            var budgets = await _budgetService.GetBudgetsByUserAsync(userId);
            return Ok(budgets);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBudget(uint id, [FromBody] PlanningBudgetDto dto)
        {
            var success = await _budgetService.UpdateBudgetAsync(id, dto);
            if (!success) return NotFound();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBudget(uint id)
        {
            var success = await _budgetService.DeleteBudgetAsync(id);
            if (!success) return NotFound();
            return Ok();
        }
    }
}
