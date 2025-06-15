using SummerPracticeWebApi.Dtos;
using SummerPracticeWebApi.Models;

namespace SummerPracticeWebApi.Mappers
{
    public class CardMapper
    {
        public static CardDto MapToDto(Card Card)
        {
            return new CardDto
            {
                Id = Card.Id,
                CardNumber = Card.CardNumber.Substring(12),
                CardHolder = Card.CardHolder,
                Validity = DateOnly.FromDateTime(Card.Validity)
            };
        }
    }
}
