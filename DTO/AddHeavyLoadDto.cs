using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace DriveHubMongo.DTO
{
    public class AddHeavyLoadDto
    {
        // Optional (Update ke time existing value use hogi)
        public string UserId { get; set; } = string.Empty;

        [Required]
        public string VehicleName { get; set; } = string.Empty;

        [Required]
        public string VehicleNumber { get; set; } = string.Empty;

        public string Category { get; set; } = "HeavyLoad";

        [Required]
        public string Location { get; set; } = string.Empty;

        [Required]
        public string OwnerName { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^[0-9]{10}$",
            ErrorMessage = "Contact number must be 10 digits")]
        public string OwnerContact { get; set; } = string.Empty;

        [Required]
        public string LoadCapacity { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        // Optional
        public IFormFile? Image { get; set; }

        // Optional
        public string PaymentMethod { get; set; } = string.Empty;

        public string PaymentStatus { get; set; } = string.Empty;

        public string TransactionId { get; set; } = string.Empty;
    }
}