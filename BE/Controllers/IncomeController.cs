using Microsoft.AspNetCore.Mvc;
using SummerPracticeWebApi.Services.Interfaces;

namespace SummerPracticeWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IncomeController : ControllerBase
    {
        private readonly IIncomeService _incomeService;

        public IncomeController(IIncomeService incomeService)
        {
            _incomeService = incomeService;
        }

        [HttpGet("{userId}/monthly")]
        public async Task<IActionResult> GetByDay(uint userId, [FromQuery] byte month, [FromQuery] uint year)
        {
            var result = await _incomeService.GetUserIncomeByDayAsync(userId, month, year);
            return Ok(result);
        }

        [HttpGet("{userId}/latest")]
        public async Task<IActionResult> GetLatestIncomes(uint userId, [FromQuery] byte month, [FromQuery] uint year)
        {
            var result = await _incomeService.GetLatestIncomesAsync(userId, month, year);
            return Ok(result);
        }
    }
}