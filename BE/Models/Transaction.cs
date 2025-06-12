using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SummerPracticeWebApi.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public bool IsIncome { get; set; }

        [Required]
        public string Currency { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        public string Description { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public bool IsExpected { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
