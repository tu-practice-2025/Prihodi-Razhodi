using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Transactions;

namespace SummerPracticeWebApi.Models
{
    [Table("categories")]
    public class Category
    {
        [Key]
        [StringLength(4)]
        public string Code { get; set; }

        public string Description { get; set; }

        public ICollection<Budget> Budgets { get; set; }
        public ICollection<TrnCode> TrnCodes { get; set; }
        public ICollection<MccCode> MccCodes { get; set; }
        public ICollection<Expense> Expenses { get; set; }
    }
}