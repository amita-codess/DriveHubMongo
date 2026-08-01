using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DriveHubMongo.Model
{
    public class Emergency
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string UserId { get; set; } = string.Empty;
        public string VehicleName { get; set; } = string.Empty;

        public string VehicleNumber { get; set; } = string.Empty;

        public string VehicleType { get; set; } = string.Empty;

        public string DriverName { get; set; } = string.Empty;

        public string DriverContact { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public string Availability { get; set; } = "Available";

        // Image URL or uploaded image path
        public string ImagePath { get; set; } =
            "/uploads/default-emergency.webp";

        public string PaymentMethod { get; set; } = "";
        public string PaymentStatus { get; set; } = "";
        public string TransactionId { get; set; } = "";
    }
}