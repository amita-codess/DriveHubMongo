using DriveHubMongo.DTO;
using DriveHubMongo.Model;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DriveHubMongo.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly IMongoCollection<Trip> _tripCollection;
        private readonly IMongoCollection<Emergency> _emergencyCollection;
        private readonly IMongoCollection<Construction> _constructionCollection;
        private readonly IMongoCollection<Agriculture> _agricultureCollection;

        public ChatRepository(IOptions<MongoDbSettings> mongoDbSettings)
        {
            var mongoClient = new MongoClient(mongoDbSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);

            _tripCollection = mongoDatabase.GetCollection<Trip>("Trips");
            _emergencyCollection = mongoDatabase.GetCollection<Emergency>("Emergency");
            _constructionCollection = mongoDatabase.GetCollection<Construction>("Construction");
            _agricultureCollection = mongoDatabase.GetCollection<Agriculture>("Agriculture");
        }

        public async Task<List<ChatSearchResultDto>> SearchVehiclesAsync(
            string? vehicleName,
            string? location)
        {
            var results = new List<ChatSearchResultDto>();

            // =========================
            // Trip Search
            // =========================

            var tripFilter = Builders<Trip>.Filter.Empty;

            if (!string.IsNullOrWhiteSpace(vehicleName))
            {
                tripFilter &= Builders<Trip>.Filter.Regex(
                    x => x.VehicleName,
                    new BsonRegularExpression(vehicleName, "i"));
            }

            if (!string.IsNullOrWhiteSpace(location))
            {
                tripFilter &= Builders<Trip>.Filter.Regex(
                    x => x.Location,
                    new BsonRegularExpression(location, "i"));
            }

            var trips = await _tripCollection.Find(tripFilter).ToListAsync();

            results.AddRange(trips.Select(x => new ChatSearchResultDto
            {
                Category = "Trip",
                VehicleName = x.VehicleName,
                VehicleNumber = x.VehicleNumber,
                Location = x.Location,
                OwnerName = x.OwnerName,
                OwnerContact = x.OwnerContact,
                ImagePath = x.ImagePath
            }));


            // =========================
            // Emergency Search
            // =========================

            var emergencyFilter = Builders<Emergency>.Filter.Empty;

            if (!string.IsNullOrWhiteSpace(vehicleName))
            {
                emergencyFilter &= Builders<Emergency>.Filter.Regex(
                    x => x.VehicleName,
                    new BsonRegularExpression(vehicleName, "i"));
            }

            if (!string.IsNullOrWhiteSpace(location))
            {
                emergencyFilter &= Builders<Emergency>.Filter.Regex(
                    x => x.Location,
                    new BsonRegularExpression(location, "i"));
            }

            var emergency = await _emergencyCollection.Find(emergencyFilter).ToListAsync();

            results.AddRange(emergency.Select(x => new ChatSearchResultDto
            {
                Category = "Emergency",
                VehicleName = x.VehicleName,
                VehicleNumber = x.VehicleNumber,
                Location = x.Location,
                OwnerName = x.DriverName,
                OwnerContact = x.DriverContact,
                ImagePath = x.ImagePath
            }));


            // =========================
            // Construction Search
            // =========================

            var constructionFilter = Builders<Construction>.Filter.Empty;

            if (!string.IsNullOrWhiteSpace(vehicleName))
            {
                constructionFilter &= Builders<Construction>.Filter.Regex(
                    x => x.VehicleName,
                    new BsonRegularExpression(vehicleName, "i"));
            }

            if (!string.IsNullOrWhiteSpace(location))
            {
                constructionFilter &= Builders<Construction>.Filter.Regex(
                    x => x.Location,
                    new BsonRegularExpression(location, "i"));
            }

            var construction = await _constructionCollection.Find(constructionFilter).ToListAsync();

            results.AddRange(construction.Select(x => new ChatSearchResultDto
            {
                Category = "Construction",
                VehicleName = x.VehicleName,
                VehicleNumber = x.VehicleNumber,
                Location = x.Location,
                OwnerName = x.UserName,
                OwnerContact = x.ContactNumber,
                ImagePath = x.ImagePath
            }));


            // =========================
            // Agriculture Search
            // =========================

            var agricultureFilter = Builders<Agriculture>.Filter.Empty;

            if (!string.IsNullOrWhiteSpace(vehicleName))
            {
                agricultureFilter &= Builders<Agriculture>.Filter.Regex(
                    x => x.VehicleName,
                    new BsonRegularExpression(vehicleName, "i"));
            }

            if (!string.IsNullOrWhiteSpace(location))
            {
                agricultureFilter &= Builders<Agriculture>.Filter.Regex(
                    x => x.Location,
                    new BsonRegularExpression(location, "i"));
            }

            var agriculture = await _agricultureCollection.Find(agricultureFilter).ToListAsync();

            results.AddRange(agriculture.Select(x => new ChatSearchResultDto
            {
                Category = "Agriculture",
                VehicleName = x.VehicleName,
                VehicleNumber = x.VehicleNumber,
                Location = x.Location,
                OwnerName = x.OwnerName,
                OwnerContact = x.OwnerContact,
                ImagePath = x.ImagePath
            }));

            return results;
        }
    }
}
