using System.ComponentModel.DataAnnotations;

namespace DriveHubMongo.DTO
{
    public class AddLightLoadDto
    {
        public string UserId { get; set; } = "";

        [Required]
        public string VehicleName { get; set; } = "";

        [Required]
        public string VehicleNumber { get; set; } = "";

        public string Category { get; set; } = "LightLoad";

        [Required]
        public string Location { get; set; } = "";

        [Required]
        public string OwnerName { get; set; } = "";

        [Required]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Invalid Contact Number")]
        public string OwnerContact { get; set; } = "";

        [Required]
        public string LoadCapacity { get; set; } = "";

        public string Description { get; set; } = "";

        public string PaymentMethod { get; set; } = "UPI";

        public string PaymentStatus { get; set; } = "Pending";

        public string TransactionId { get; set; } = "";
    }
}
