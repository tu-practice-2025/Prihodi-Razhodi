using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SummerPracticeWebApi.Models
{
    [Table("budgets")]
    public class Budget
    {
        [Key]
        public int Id { get; set; }

        public int Amount { get; set; }
        public Currency Currency { get; set; }
        public bool IsIncome { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int UserId { get; set; }
        public required User User { get; set; }

        public string CategoryCode { get; set; }
        public Category Category { get; set; }
    }
}
