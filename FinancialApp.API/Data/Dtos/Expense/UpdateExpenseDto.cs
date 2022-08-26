using FinancialApp.API.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace FinancialApp.API.Data.Dtos.Expense
{
    public class UpdateExpenseDto
    {
        [Required(ErrorMessage = "Expense must have a description")]
        [StringLength(30, ErrorMessage = "Expense's description has more than 30 characters")]
        public string ExpenseName { get; set; }
        [Required(ErrorMessage = "Expense must have a certain amount")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be positive")]
        public double ExpenseAmount { get; set; }
        [Required(ErrorMessage = "Expense must have a date")]
        public DateTime ExpenseDate { get; set; }
        [Required(ErrorMessage = "Must chose a category for expense")]
        public ExpenseCategory Category { get; set; }
    }
}
