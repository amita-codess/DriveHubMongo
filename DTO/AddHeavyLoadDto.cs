using System.ComponentModel.DataAnnotations;

namespace DriveHubMongo.DTO
{
    public class AddHeavyLoadDto
    {

        public string UserId { get; set; }


        [Required]
        public string VehicleName { get; set; }


        [Required]
        public string VehicleNumber { get; set; }


        public string Category { get; set; } = "HeavyLoad";


        [Required]
        public string Location { get; set; }


        [Required]
        public string OwnerName { get; set; }


        [Required]
        [RegularExpression(@"^[0-9]{10}$",
        ErrorMessage = "Contact number must be 10 digits")]
        public string OwnerContact { get; set; }



        [Required]
        public string LoadCapacity { get; set; }
        // Example: 10 Ton, 20 Ton


        public string Description { get; set; }



        public string PaymentMethod { get; set; }


        public string PaymentStatus { get; set; }


        public string TransactionId { get; set; }

    }
}
