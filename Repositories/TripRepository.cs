using DriveHubMongo.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DriveHubMongo.Repositories
{
    public class TripRepository : ITripRepository
    {
        private readonly IMongoCollection<Trip> _tripCollection;

        public TripRepository(IOptions<MongoDbSettings> mongoDbSettings)
        {
            var mongoClient = new MongoClient(mongoDbSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);

            _tripCollection = mongoDatabase.GetCollection<Trip>("Trips");
        }

        public async Task<List<Trip>> GetAllTripsAsync()
        {
            return await _tripCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Trip?> GetTripByIdAsync(string id)
        {
            return await _tripCollection.Find(t => t.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddTripAsync(Trip trip)
        {
            await _tripCollection.InsertOneAsync(trip);
        }

        public async Task UpdateTripAsync(string id, Trip trip)
        {
            await _tripCollection.ReplaceOneAsync(t => t.Id == id, trip);
        }

        public async Task DeleteTripAsync(string id)
        {
            await _tripCollection.DeleteOneAsync(t => t.Id == id);
        }

        public async Task<List<Trip>> GetTripsByUserId(string userId)
        {
            return await _tripCollection
                .Find(x => x.UserId == userId)
                .ToListAsync();
        }
    }
}