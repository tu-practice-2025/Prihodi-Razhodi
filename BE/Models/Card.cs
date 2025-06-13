using SummerPracticeWebApi.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SummerPracticeWebApi.Models
{
    [Table("cards")]
    public class Card
    {
        [Key]
        public int Id { get; set; }

        [StringLength(16)]
        public string CardNumber { get; set; }

        public string CardHolder { get; set; }
        public DateTime Validity { get; set; }

        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}
