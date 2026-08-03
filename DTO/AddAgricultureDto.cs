using System.ComponentModel.DataAnnotations;

namespace DriveHubMongo.DTO
{
    public class AddAgricultureDto
    {
        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public string VehicleName { get; set; } = string.Empty;

        [Required]
        public string VehicleNumber { get; set; } = string.Empty;

        [Required]
        public string Category { get; set; } = string.Empty;

        [Required]
        public string Location { get; set; } = string.Empty;

        [Required]
        public string OwnerName { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^\d{10}$",
            ErrorMessage = "Owner Contact must be exactly 10 digits.")]
        public string OwnerContact { get; set; } = string.Empty;

        [Required]
        public string Specifications { get; set; } = string.Empty;

        public IFormFile? Image { get; set; }

        public string PaymentMethod { get; set; } = "";

        public string PaymentStatus { get; set; } = "";

        public string TransactionId { get; set; } = "";
    }
}