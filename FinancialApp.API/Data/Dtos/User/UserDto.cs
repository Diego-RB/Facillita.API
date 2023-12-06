using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Facillita.API.Data.Dtos.User
{
    public class UserDto
    {
        [Required]
        [DataMember]
        public string Username { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        [DataMember]
        public string Email { get; set; }

        [Required]
        [DataMember]
        public string UID { get; set; }
    }
}
