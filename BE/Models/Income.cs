using SummerPracticeWebApi.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SummerPracticeWebApi.Models
{
    [Table("incomes")]
    public class Income
    {
        [Key]
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        public int AccountId { get; set; }
        public Account Account { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}