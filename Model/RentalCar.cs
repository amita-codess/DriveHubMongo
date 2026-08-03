using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DriveHubMongo.Model
{
    public class RentalCar
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        public string UserId { get; set; } = "";

        public string VehicleName { get; set; } = "";

        public string VehicleNumber { get; set; } = "";

        public string Category { get; set; } = "RentalCar";

        public string Location { get; set; } = "";

        public string OwnerName { get; set; } = "";

        public string OwnerContact { get; set; } = "";

        public int SeatingCapacity { get; set; }
        public bool ACAvailable { get; set; }

        public string ImagePath { get; set; } = "/uploads/default-rentalcar.webp";

        public string PaymentMethod { get; set; } = "";

        public string PaymentStatus { get; set; } = "Pending";

        public string TransactionId { get; set; } = "";
    }
}
