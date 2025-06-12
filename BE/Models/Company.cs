using System.ComponentModel.DataAnnotations;
using System.Transactions;

namespace SummerPracticeWebApi.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string ColorHex { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}