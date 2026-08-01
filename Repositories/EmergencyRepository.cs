using DriveHubMongo.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DriveHubMongo.Repositories
{
    public class EmergencyRepository : IEmergencyRepository
    {
        private readonly IMongoCollection<Emergency> _emergencyCollection;

        public EmergencyRepository(IOptions<MongoDbSettings> mongoDbSettings)
        {
            var mongoClient = new MongoClient(mongoDbSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);

            _emergencyCollection = mongoDatabase.GetCollection<Emergency>("Emergency");
        }

        public async Task<List<Emergency>> GetAllEmergencyAsync()
        {
            return await _emergencyCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Emergency?> GetEmergencyByIdAsync(string id)
        {
            return await _emergencyCollection
                .Find(e => e.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task AddEmergencyAsync(Emergency emergency)
        {
            await _emergencyCollection.InsertOneAsync(emergency);
        }

        public async Task UpdateEmergencyAsync(string id, Emergency emergency)
        {
            await _emergencyCollection.ReplaceOneAsync(e => e.Id == id, emergency);
        }

        public async Task DeleteEmergencyAsync(string id)
        {
            await _emergencyCollection.DeleteOneAsync(e => e.Id == id);
        }

        public async Task DeleteAllEmergencyAsync()
        {
            await _emergencyCollection.DeleteManyAsync(_ => true);
        }

        public async Task<List<Emergency>> GetEmergencyByUserId(string userId)
        {
            return await _emergencyCollection
                .Find(x => x.UserId == userId)
                .ToListAsync();
        }
    }
}