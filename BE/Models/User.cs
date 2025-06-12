using System.ComponentModel.DataAnnotations;
using System.Transactions;

namespace SummerPracticeWebApi.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}