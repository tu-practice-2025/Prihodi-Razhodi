using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using SummerPracticeWebApi.Dtos;
using SummerPracticeWebApi.Services.Interfaces;

namespace SummerPracticeWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        // GET: api/card/account/5
        [HttpGet("account/{accountId}")]
        public async Task<ActionResult<IEnumerable<CardDto>>> GetCardsByAccountId(uint accountId)
        {
            var cards = await _cardService.GetCardsByAccountId(accountId);

            if (!cards.Any())
            {
                return NotFound($"No cards found for account with ID {accountId}.");
            }

            return Ok(cards);
        }
    }
}
