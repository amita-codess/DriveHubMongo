using System.ComponentModel.DataAnnotations;

namespace DriveHubMongo.DTO
{
    public class RegisterDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Format")]
        public string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{10}$",
            ErrorMessage = "Mobile Number must be 10 digits")]
        public string MobileNumber { get; set; }
    }
}