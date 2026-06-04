using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.AccountDTOs
{
    public class RegisterDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string FirstName { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string LastName { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        [RegularExpression("^[a-zA-Z0-9]+$")]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [MinLength(8)]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).+$")]
        public string Password { get; set; } = string.Empty;
    }
}
