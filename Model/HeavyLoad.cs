using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DriveHubMongo.Model
{
    public class HeavyLoad
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }


        public string UserId { get; set; }


        public string VehicleName { get; set; }


        public string VehicleNumber { get; set; }


        public string Category { get; set; } = "HeavyLoad";


        public string Location { get; set; }


        public string OwnerName { get; set; }


        public string OwnerContact { get; set; }


        public string LoadCapacity { get; set; }


        public string Description { get; set; }


        public string ImagePath { get; set; } =
        "/uploads/default-heavyload.webp";


        public string PaymentMethod { get; set; }


        public string PaymentStatus { get; set; }


        public string TransactionId { get; set; }


    }
}
