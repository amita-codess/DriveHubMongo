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
        private readonly IMongoCollection<HeavyLoad> _heavyLoadCollection;
        private readonly IMongoCollection<LightLoad> _lightLoadCollection;
        private readonly IMongoCollection<RentalCar> _rentalCarCollection;

        public ChatRepository(IOptions<MongoDbSettings> mongoDbSettings)
        {
            var mongoClient = new MongoClient(mongoDbSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);

            _tripCollection = mongoDatabase.GetCollection<Trip>("Trips");
            _emergencyCollection = mongoDatabase.GetCollection<Emergency>("Emergency");
            _constructionCollection = mongoDatabase.GetCollection<Construction>("Construction");
            _agricultureCollection = mongoDatabase.GetCollection<Agriculture>("Agriculture");
            _heavyLoadCollection = mongoDatabase.GetCollection<HeavyLoad>("HeavyLoads");
            _lightLoadCollection = mongoDatabase.GetCollection<LightLoad>("LightLoads");
            _rentalCarCollection = mongoDatabase.GetCollection<RentalCar>("RentalCars");
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


            // =========================
            // Heavy Load Search
            // =========================

            var heavyLoadFilter = Builders<HeavyLoad>.Filter.Empty;

            if (!string.IsNullOrWhiteSpace(vehicleName))
            {
                heavyLoadFilter &= Builders<HeavyLoad>.Filter.Regex(
                    x => x.VehicleName,
                    new BsonRegularExpression(vehicleName, "i"));
            }

            if (!string.IsNullOrWhiteSpace(location))
            {
                heavyLoadFilter &= Builders<HeavyLoad>.Filter.Regex(
                    x => x.Location,
                    new BsonRegularExpression(location, "i"));
            }

            var heavyLoads = await _heavyLoadCollection.Find(heavyLoadFilter).ToListAsync();

            results.AddRange(heavyLoads.Select(x => new ChatSearchResultDto
            {
                Category = "Heavy Load",
                VehicleName = x.VehicleName,
                VehicleNumber = x.VehicleNumber,
                Location = x.Location,
                OwnerName = x.OwnerName,
                OwnerContact = x.OwnerContact,
                ImagePath = x.ImagePath
            }));


            // =========================
            // Light Load Search
            // =========================

            var lightLoadFilter = Builders<LightLoad>.Filter.Empty;

            if (!string.IsNullOrWhiteSpace(vehicleName))
            {
                lightLoadFilter &= Builders<LightLoad>.Filter.Regex(
                    x => x.VehicleName,
                    new BsonRegularExpression(vehicleName, "i"));
            }

            if (!string.IsNullOrWhiteSpace(location))
            {
                lightLoadFilter &= Builders<LightLoad>.Filter.Regex(
                    x => x.Location,
                    new BsonRegularExpression(location, "i"));
            }

            var lightLoads = await _lightLoadCollection.Find(lightLoadFilter).ToListAsync();

            results.AddRange(lightLoads.Select(x => new ChatSearchResultDto
            {
                Category = "Light Load",
                VehicleName = x.VehicleName,
                VehicleNumber = x.VehicleNumber,
                Location = x.Location,
                OwnerName = x.OwnerName,
                OwnerContact = x.OwnerContact,
                ImagePath = x.ImagePath
            }));


            // =========================
            // Rental Cars Search
            // =========================

            var rentalCarFilter = Builders<RentalCar>.Filter.Empty;

            if (!string.IsNullOrWhiteSpace(vehicleName))
            {
                rentalCarFilter &= Builders<RentalCar>.Filter.Regex(
                    x => x.VehicleName,
                    new BsonRegularExpression(vehicleName, "i"));
            }

            if (!string.IsNullOrWhiteSpace(location))
            {
                rentalCarFilter &= Builders<RentalCar>.Filter.Regex(
                    x => x.Location,
                    new BsonRegularExpression(location, "i"));
            }

            var rentalCars = await _rentalCarCollection.Find(rentalCarFilter).ToListAsync();

            results.AddRange(rentalCars.Select(x => new ChatSearchResultDto
            {
                Category = "Rental Cars",
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
