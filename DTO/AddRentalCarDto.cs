using System.ComponentModel.DataAnnotations;

namespace DriveHubMongo.DTO
{
    public class AddRentalCarDto
    {
        public string UserId { get; set; } = "";


        [Required]
        public string VehicleName { get; set; } = "";


        [Required]
        public string VehicleNumber { get; set; } = "";


        public string Category { get; set; } = "RentalCar";


        [Required]
        public string Location { get; set; } = "";


        [Required]
        public string OwnerName { get; set; } = "";


        [Required]
        [RegularExpression(@"^[0-9]{10}$",
        ErrorMessage = "Enter valid 10 digit mobile number")]
        public string OwnerContact { get; set; } = "";


        [Range(1, 10)]
        public int SeatingCapacity { get; set; }

        public bool ACAvailable { get; set; }

        public IFormFile? Image { get; set; }


        public string PaymentMethod { get; set; } = "";


        public string PaymentStatus { get; set; } = "Pending";


        public string TransactionId { get; set; } = "";
    }
}
