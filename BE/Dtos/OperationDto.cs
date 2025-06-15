using SummerPracticeWebApi.Enums;

namespace SummerPracticeWebApi.Dtos
{
    public class OperationDto
    {
        public uint Id { get; set; }
        public decimal AmountLcy { get; set; }
        public decimal AmountFcy { get; set; }
        public Currency LocalCurrency { get; set; }
        public Currency ForeignCurrency { get; set; }
        public DateTime DateTime { get; set; }
        public string Description { get; set; }
        public string OwnIban { get; set; }
        public string OwnName { get; set; }
        public string OtherIban { get; set; }
        public string OtherName { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryDescription { get; set; }
        public bool IsExpense { get; set; }
        public string CardNumber { get; set; }
        public string AccountDescription { get; set; }
    }
}
