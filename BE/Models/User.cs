using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Transactions;

namespace SummerPracticeWebApi.Models
{

    [Index(nameof(Username), IsUnique = true)]
    [Index(nameof(EGN), IsUnique = true)]
    [Table("users")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required string Username { get; set; }
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string MiddleName { get; set; }
        [Required]
        public required string LastName { get; set; }
        [Required]
        public required string EGN { get; set; }


        public ICollection<Account> Accounts { get; set; }
        public ICollection<Budget> Budgets { get; set; }
    }
}