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
    }
}
