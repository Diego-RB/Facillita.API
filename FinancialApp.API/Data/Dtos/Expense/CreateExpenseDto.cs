using Facillita.API.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Facillita.API.Data.Dtos.Expense
{
    public class CreateExpenseDto
    {
        [Required(ErrorMessage = "Expense must have a description")]
        [StringLength(30, ErrorMessage = "Expense's description has more than 30 characters")]
        [DataMember]
        public string ExpenseName { get; set; }

        [Required(ErrorMessage = "Expense must have a certain amount")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be positive")]
        [DataMember]
        public double ExpenseAmount { get; set; }

        [Required(ErrorMessage = "Expense must have a date")]
        [DataMember]
        public DateTime ExpenseDate { get; set; }

        [Required(ErrorMessage = "Must chose a category for expense")]
        [DataMember]
        public ExpenseCategory Category { get; set; }

        [Required]
        [DataMember]
        public string UserUID { get; set; }
    }
}
