using System.ComponentModel.DataAnnotations;

namespace FinancialAppAPI.Models
{
    public class Income
    {
        [Key]
        [Required]
        public int incomeId { get; private set; }
        [Required(ErrorMessage = "Income must have a description")]
        [StringLength(30, ErrorMessage = "Income's description has more than 30 characters")]
        public string incomeName { get; set; }
        [Required(ErrorMessage = "Income must have a certain amount")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be positive")]
        public double incomeAmount { get; set; }
        [Required(ErrorMessage = "Income must have a date")]
        public DateTime incomeDate { get; set; }

    }
}
