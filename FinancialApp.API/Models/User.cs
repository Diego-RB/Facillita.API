using System.ComponentModel.DataAnnotations;

namespace Facillita.API.Models
{
    public class User
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string UID { get; set; }

        public ICollection<Income> Incomes { get; set; }

        public ICollection<Expense> Expenses { get; set; }
    }
}
