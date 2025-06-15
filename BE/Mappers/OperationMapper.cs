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
                Description = Operation.Description ?? "No Desctiption",
                OwnIban = Operation.OwnIban ?? "No Own IBAN",
                OwnName = Operation.OwnName ?? "No Own Name",
                OtherIban = Operation.OtherIban ?? "No Other IBAN",
                OtherName = Operation.OtherName ?? "No Other Name",
                CategoryCode = Operation.CategoryCode ?? "No Category",
                CategoryDescription = Operation.CategoryCodeNavigation?.Description ?? "No Description",
                IsExpense = Operation.IsExpense,
                CardNumber = Operation.Card?.CardNumber != null
                    ? Operation.Card.CardNumber.Substring(12)
                    : "NoCard",
                AccountDescription = Operation.Acc.Description ?? "No Description"
            };
        }
    }
}
