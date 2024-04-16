using System.ComponentModel.DataAnnotations;

namespace Reactivities.API.DTOs
{
    public class RegisterDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression("(?=.*[^A-Za-z])(?=.*[A-Z])(?=.*[a-z]).{4,}$", ErrorMessage = "Password must be complex")]
        public string Password { get; set; }

        [Required]
        public string DisplayName { get; set; }

        [Required]
        public string Username { get; set; }
    }
}
