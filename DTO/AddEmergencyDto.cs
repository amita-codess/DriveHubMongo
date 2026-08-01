using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace DriveHubMongo.DTO
{
    public class AddEmergencyDto
    {
        public string UserId { get; set; } = string.Empty;

        [Required]
        public string VehicleName { get; set; } = string.Empty;

        [Required]
        public string VehicleNumber { get; set; } = string.Empty;

        [Required]
        public string VehicleType { get; set; } = string.Empty;

        [Required]
        public string DriverName { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Mobile number must be 10 digits.")]
        public string DriverContact { get; set; } = string.Empty;

        [Required]
        public string Location { get; set; } = string.Empty;

        [Required]
        public string Availability { get; set; } = "Available";

        // Optional Image Upload
        public IFormFile? Image { get; set; }

        public string PaymentMethod { get; set; } = "";
        public string PaymentStatus { get; set; } = "";
        public string TransactionId { get; set; } = "";
    }
}