using System.ComponentModel.DataAnnotations;

namespace Facillita.Users.Data.Requests
{
    public class LoginRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }    
    }
}
