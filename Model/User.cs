using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DriveHubMongo.Model
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; } = string.Empty;

        [BsonElement("email")]
        public string Email { get; set; } = string.Empty;

        [BsonElement("passwordHash")]
        public string PasswordHash { get; set; } = string.Empty;

        [BsonElement("mobileNumber")]
        public string MobileNumber { get; set; } = string.Empty;

        [BsonElement("role")]
        public string Role { get; set; } = "User";
    }
}