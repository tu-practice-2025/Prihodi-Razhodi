using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Transactions;

namespace SummerPracticeWebApi.Models
{
    [Table("trn_codes")]
    public class TrnCode
    {
        [Key]
        [StringLength(3)]
        public string Code { get; set; }

        public string TrnDesc { get; set; }

        public string CategoryCode { get; set; }
        public Category Category { get; set; }

        public ICollection<Expense> Expenses { get; set; }
        public ICollection<Income> Incomes { get; set; }
    }
}