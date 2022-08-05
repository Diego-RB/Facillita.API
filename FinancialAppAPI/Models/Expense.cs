using System.ComponentModel.DataAnnotations;

namespace FinancialAppAPI.Models
{
    public class Expense
    {
        [Key]
        [Required]
        public int expenseId { get; private set; }
        [Required(ErrorMessage = "Expense must have a description")]
        [StringLength(30, ErrorMessage = "Expense's description has more than 30 characters")]
        public string expenseName { get; set; }
        [Required(ErrorMessage = "Expense must have a certain amount")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be positive")] 
        public double expenseAmount { get; set; }
        [Required(ErrorMessage = "Expense must have a date")]
        public DateTime expenseDate { get; set; }

    }
}
