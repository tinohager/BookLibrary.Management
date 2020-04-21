using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Management.WebApi.Model
{
    public class AuthenticationCredentialsDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
