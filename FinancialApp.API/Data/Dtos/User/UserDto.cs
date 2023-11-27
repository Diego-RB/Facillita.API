using System.ComponentModel.DataAnnotations;

namespace Facillita.API.Data.Dtos.User
{
    public class UserDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Required]
        public string UID { get; set; }
    }
}
