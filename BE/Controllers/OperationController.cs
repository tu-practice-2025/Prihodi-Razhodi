using Microsoft.AspNetCore.Mvc;
using SummerPracticeWebApi.Services.Interfaces;

namespace SummerPracticeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        private readonly IOperationService _operationService;

        public OperationController(IOperationService operationService)
        {
            _operationService = operationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOperations(
            [FromQuery] uint userId,
            [FromQuery] int year,
            [FromQuery] int month)
        {
            if (userId > 0 && year > 0 && month > 0)
            {
                return Ok(await _operationService.GetOperationsByUserIdYearAndMonth(userId, month, year));
            } 
            else if (userId > 0 && year > 0)
            {
                return Ok(await _operationService.GetOperationsByUserIdAndYear(userId, year));
            }
            else if (userId > 0)
            {
                return Ok(await _operationService.GetOperationsByUserId(userId));
            }
            else
            {
                return NotFound();
            }
               
        }
    }
}
