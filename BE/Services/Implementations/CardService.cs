using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using SummerPracticeWebApi.DataAccess.Context;
using SummerPracticeWebApi.Dtos;
using SummerPracticeWebApi.Mappers;
using SummerPracticeWebApi.Services.Interfaces;

namespace SummerPracticeWebApi.Services.Implementations
{
    public class CardService : ICardService
    {
        private readonly IncomeExpensesContext _context;

        public CardService(IncomeExpensesContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CardDto>> GetCardsByAccountId(uint accountId)
        {
            var cards = await _context.Cards
                .Where(c => c.AccountId == accountId)
                .ToListAsync();

            return cards.Select(CardMapper.MapToDto);
        }
    }
}
