using SummerPracticeWebApi.Dtos;
using SummerPracticeWebApi.Models;

namespace SummerPracticeWebApi.Mappers
{
    public class OperationMapper
    {
        public static OperationDto MapToDto(Operation Operation)
        {
            return new OperationDto
            {
                Id = Operation.Id,
                AmountLcy = Operation.AmountLcy,
                AmountFcy = Operation.AmountFcy,
                LocalCurrency = Operation.LocalCurrency,
                ForeignCurrency = Operation.ForeignCurrency,
                DateTime = Operation.DateTime,
                Description = Operation.Description,
                OwnIban = Operation.OwnIban,
                OwnName = Operation.OwnName,
                OtherIban = Operation.OtherIban,
                OtherName = Operation.OtherName,
                CategoryCode = Operation.CategoryCode,
                CategoryDescription = Operation.CategoryCodeNavigation.Description,
                IsExpense = Operation.IsExpense,
                CardNumber = Operation.Card.CardNumber.Substring(12),
                AccountDescription = Operation.Acc.Description
            };
        }
    }
}
