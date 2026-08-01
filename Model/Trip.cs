using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DriveHubMongo.Model
{
    public class Trip
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string UserId { get; set; } = string.Empty;
        public string VehicleName { get; set; } = string.Empty;

        public string VehicleNumber { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public string OwnerName { get; set; } = string.Empty;

        public string OwnerContact { get; set; } = string.Empty;

        public int SeatingCapacity { get; set; }

        public bool ACAvailable { get; set; }

        // Image URL or uploaded image path
        public string ImagePath { get; set; } =
            "/uploads/default-trip.webp";

        public string PaymentMethod { get; set; } = "";
        public string PaymentStatus { get; set; } = "";
        public string TransactionId { get; set; } = "";
    }
}