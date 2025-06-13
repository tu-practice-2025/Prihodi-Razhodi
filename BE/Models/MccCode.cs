using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Transactions;

namespace SummerPracticeWebApi.Models
{
    [Table("mcc_codes")]
    public class MccCode
    {
        [Key]
        [StringLength(4)]
        public string Code { get; set; }

        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public string CategoryCode { get; set; }
        public Category Category { get; set; }

        public ICollection<Expense> Expenses { get; set; }
        public ICollection<Income> Incomes { get; set; }
    }

}