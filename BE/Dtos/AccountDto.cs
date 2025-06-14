using SummerPracticeWebApi.Enums;

namespace SummerPracticeWebApi.Dtos
{
    public class AccountDto
    {
        public uint Id { get; set; }
        public string Iban { get; set; }
        public decimal balance { get; set; }
        public Currency Currency { get; set; }
    }
}
