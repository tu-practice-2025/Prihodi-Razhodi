using Microsoft.AspNetCore.Mvc;
using SummerPracticeWebApi.Services.Interfaces;

namespace SummerPracticeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AiResponseController : ControllerBase
    {
        private readonly IAiResponseService _aiResponseService;

        public AiResponseController(IAiResponseService aiResponseService)
        {
            _aiResponseService = aiResponseService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetResponse(uint userId)
        {
            return Ok(await _aiResponseService.GetResponse(userId));
        }
    }
}
