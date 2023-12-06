using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Facillita.API.Data.Dtos.Income
{
    public class CreateIncomeDto
    {
        [Required(ErrorMessage = "Income must have a description")]
        [StringLength(30, ErrorMessage = "Income's description has more than 30 characters")]
        [DataMember]
        public string IncomeName { get; set; }

        [Required(ErrorMessage = "Income must have a certain amount")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be positive")]
        [DataMember]
        public double IncomeAmount { get; set; }

        [Required(ErrorMessage = "Income must have a date")]
        [DataMember]
        public DateTime IncomeDate { get; set; }

        [Required]
        [DataMember]
        public string UserUID { get; set; }
    }
}
