using SummerPracticeWebApi.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SummerPracticeWebApi.Models
{
    [Table("expenses")]
    public class Expense
    {
        [Key]
        public int Id { get; set; }

        public decimal AmountLcy { get; set; }
        public decimal AmountFcy { get; set; }

        public Currency LocalCurrency { get; set; }
        public Currency ForeignCurrency { get; set; }

        public DateTime DateTime { get; set; } = System.DateTime.UtcNow;
        public string Description { get; set; }

        public string RecipientIban { get; set; }
        public string RecipientName { get; set; }
        public string SenderIban { get; set; }
        public string SenderName { get; set; }

        public string CategoryCode { get; set; }
        public Category Category { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int AccId { get; set; }
        public Account Account { get; set; }

        public int CardId { get; set; }
        public Card Card { get; set; }

        public string TrnCode { get; set; }
        public TrnCode TrnCodeRef { get; set; }
    }

}