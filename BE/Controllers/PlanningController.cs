using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SummerPracticeWebApi.DataAccess.Context;
using SummerPracticeWebApi.Models;

namespace SummerPracticeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanningController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PlanningController(AppDbContext context)
        {
            _context = context;
        }

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
    }
}
