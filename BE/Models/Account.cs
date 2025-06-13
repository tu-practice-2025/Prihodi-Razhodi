using SummerPracticeWebApi.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SummerPracticeWebApi.Models
{
    [Table("accounts")]
    public class Account
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string IBAN { get; set; }

        [Required]
        public decimal Balance { get; set; }

        [Required]
        public Currency Currency { get; set; }


        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<Card> Cards { get; set; }
        public ICollection<Expense> Expenses { get; set; }
        public ICollection<Income> Incomes { get; set; }
    }
}