using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SummerPracticeWebApi.DataAccess.Context;
using SummerPracticeWebApi.Enums;
using SummerPracticeWebApi.Models;

namespace SummerPracticeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanningController : ControllerBase
    {
        private readonly IncomeExpensesContext _context;

        public PlanningController(IncomeExpensesContext context)
        {
            _context = context;
        }


        // Budget post api
        [HttpPost]
        public async Task<IActionResult> AddBudget([FromBody] PlanningBudgetDto dto)
        {
            var budget = new Budget
            {
                Amount = dto.Amount,
                Currency = dto.Currency,
                IsIncome = dto.IsIncome,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                CategoryCode = dto.CategoryCode,
                UserId = dto.UserId,
            };

            _context.Budgets.Add(budget);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // Budget get api, search by id
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetBudgetsByUser(int userId)
        {
            var budgets = await _context.Budgets
                .Where(b => b.UserId == userId)
                .Select(b => new
                {
                    b.Amount,
                    Currency = ((Currency)b.Currency).ToString(),
                    b.IsIncome,
                    StartDate = b.StartDate.ToString("yyyy-MM-dd"),
                    EndDate = b.EndDate.ToString("yyyy-MM-dd"),
                    b.CategoryCode
                })
                .ToListAsync();

            return Ok(budgets);
        }

        // Budget put api, edit by id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBudget(int id, [FromBody] PlanningBudgetDto dto)
        {
            var budget = await _context.Budgets.FindAsync(id);
            if (budget == null) return NotFound();

            budget.Amount = dto.Amount;
            budget.Currency = dto.Currency;
            budget.IsIncome = dto.IsIncome;
            budget.StartDate = dto.StartDate;
            budget.EndDate = dto.EndDate;
            budget.CategoryCode = dto.CategoryCode;

            await _context.SaveChangesAsync();
            return Ok();
        }

        // Budget delete api, delete by id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBudget(int id)
        {
            var budget = await _context.Budgets.FindAsync(id);
            if (budget == null) return NotFound();

            _context.Budgets.Remove(budget);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
