using Microsoft.AspNetCore.Mvc;
using SummerPracticeWebApi.Services.Interfaces;
using System.Threading.Tasks;

namespace SummerPracticeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SummaryController : ControllerBase
    {
        private readonly ISummaryService _summaryService;

        public SummaryController(ISummaryService summaryService)
        {
            _summaryService = summaryService;
        }

        [HttpGet("expenses")]
        public async Task<IActionResult> GetExpensesSummary(
            [FromQuery] uint userId,
            [FromQuery] int month,
            [FromQuery] int year)
        {
            return Ok(await _summaryService.getExpensesByMonthAndYear(userId, month, year));
        }

        [HttpGet("balance/{userId}")]
        public async Task<IActionResult> GetBalanceSummary(uint userId)
        {
            return Ok(await _summaryService.getBalanceSummary(userId));
        }
    }
}
