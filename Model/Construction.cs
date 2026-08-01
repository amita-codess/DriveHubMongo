using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DriveHubMongo.Model
{
    public class Construction
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string UserId { get; set; } = string.Empty;
        public string VehicleName { get; set; } = string.Empty;

        public string VehicleNumber { get; set; } = string.Empty;

        public string WorkType { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public string ContactNumber { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public string Status { get; set; } = "Available";

        public string Description { get; set; } = string.Empty;

        public string ImagePath { get; set; } = "/uploads/default-construction.webp";

        public string PaymentMethod { get; set; } = "";
        public string PaymentStatus { get; set; } = "";
        public string TransactionId { get; set; } = "";
    }
}