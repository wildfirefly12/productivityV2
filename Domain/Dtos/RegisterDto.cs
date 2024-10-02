using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos
{

    public class RegisterDto
    {
        [Required] public string Username { get; set; }
        [Required] [EmailAddress] public string Email { get; set; }

        [Required]
        [RegularExpression("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{8,20}$", ErrorMessage = "Password is not strong enough.")]
        public string Password { get; set; }
    }
}