using SummerPracticeWebApi.Dtos;
using SummerPracticeWebApi.Models;

namespace SummerPracticeWebApi.Mappers
{
    public class AccountMapper
    {
        public static AccountDto MapToDto(Account Account)
        {
            return new AccountDto
            {
                Id = Account.Id,
                Iban = Account.Iban,
                balance = Account.Balance,
                Currency = Account.Currency
            };
        }
    }
}
