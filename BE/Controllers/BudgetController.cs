using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SummerPracticeWebApi.DataAccess.Context;
using SummerPracticeWebApi.Dtos.Budget;
using SummerPracticeWebApi.Enums;
using SummerPracticeWebApi.Models;

namespace SummerPracticeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetController : ControllerBase
    {
        private readonly IncomeExpensesContext _context;

        public BudgetController(IncomeExpensesContext context)
        {
            _context = context;
        }


        // Budget post api
        [HttpPost]
        public async Task<IActionResult> AddBudget([FromBody] BudgetDto dto)
        {
            var budget = new Budget
            {
                Amount = dto.Amount,
                Currency = dto.Currency,
                Month = dto.Month,
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
                .ToListAsync();

            return Ok(budgets);
        }

        // Budget put api, edit by id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBudget(int id, [FromBody] BudgetDto dto)
        {
            var budget = await _context.Budgets.FindAsync(id);
            if (budget == null) return NotFound();

            budget.Amount = dto.Amount;
            budget.Currency = dto.Currency;
            budget.Month = dto.Month;
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
