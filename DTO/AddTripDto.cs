using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace DriveHubMongo.DTO
{
    public class AddTripDto
    {
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
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Mobile number must be 10 digits.")]
        public string OwnerContact { get; set; } = string.Empty;

        [Range(1, 100)]
        public int SeatingCapacity { get; set; }

        public bool ACAvailable { get; set; }

        // Optional Image Upload
        public IFormFile? Image { get; set; }

        public string PaymentMethod { get; set; } = "";
        public string PaymentStatus { get; set; } = "";
        public string TransactionId { get; set; } = "";
    }
}