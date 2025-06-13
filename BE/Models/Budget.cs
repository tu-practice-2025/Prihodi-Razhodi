using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SummerPracticeWebApi.Models
{
    [Table("budgets")]
    public class Budget
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("amount")]
        public int Amount { get; set; }
        public Currency Currency { get; set; }

        [Column("is_income")]
        public bool IsIncome { get; set; }

        [Column("start_date")]
        public DateTime StartDate { get; set; }
        [Column("end_date")]
        public DateTime EndDate { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }
        public User User { get; set; }

        [Column("category_code")]
        public string CategoryCode { get; set; }
        public Category Category { get; set; }
    }
}
