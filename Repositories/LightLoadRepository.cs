using DriveHubMongo.Model;
using MongoDB.Driver;

namespace DriveHubMongo.Repositories
{
    public class LightLoadRepository : ILightLoadRepository
    {
        private readonly IMongoCollection<LightLoad> _collection;

        public LightLoadRepository(IConfiguration configuration)
        {
            var client = new MongoClient(
                configuration["MongoDbSettings:ConnectionString"]
            );

            var database = client.GetDatabase(
                configuration["MongoDbSettings:DatabaseName"]
            );

            _collection = database.GetCollection<LightLoad>("LightLoad");
        }


        public async Task<List<LightLoad>> GetAllAsync()
        {
            return await _collection
                .Find(x => true)
                .ToListAsync();
        }


        public async Task<LightLoad?> GetByIdAsync(string id)
        {
            return await _collection
                .Find(x => x.Id == id)
                .FirstOrDefaultAsync();
        }


        public async Task<List<LightLoad>> GetByUserIdAsync(string userId)
        {
            return await _collection
                .Find(x => x.UserId == userId)
                .ToListAsync();
        }


        public async Task CreateAsync(LightLoad lightLoad)
        {
            await _collection.InsertOneAsync(lightLoad);
        }


        public async Task UpdateAsync(string id, LightLoad lightLoad)
        {
            await _collection.ReplaceOneAsync(
                x => x.Id == id,
                lightLoad
            );
        }


        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(
                x => x.Id == id
            );
        }
    }
}