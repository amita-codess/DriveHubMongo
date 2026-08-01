using DriveHubMongo.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DriveHubMongo.Repositories
{
    public class ConstructionRepository : IConstructionRepository
    {
        private readonly IMongoCollection<Construction> _constructionCollection;

        public ConstructionRepository(IOptions<MongoDbSettings> mongoDbSettings)
        {
            var mongoClient = new MongoClient(mongoDbSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);

            _constructionCollection =
                mongoDatabase.GetCollection<Construction>("Construction");
        }

        public async Task<List<Construction>> GetAllConstructionAsync()
        {
            return await _constructionCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Construction?> GetConstructionByIdAsync(string id)
        {
            return await _constructionCollection
                .Find(c => c.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task AddConstructionAsync(Construction construction)
        {
            await _constructionCollection.InsertOneAsync(construction);
        }

        public async Task UpdateConstructionAsync(string id, Construction construction)
        {
            await _constructionCollection.ReplaceOneAsync(
                c => c.Id == id,
                construction
            );
        }

        public async Task DeleteConstructionAsync(string id)
        {
            await _constructionCollection.DeleteOneAsync(c => c.Id == id);
        }

        public async Task DeleteAllConstructionAsync()
        {
            await _constructionCollection.DeleteManyAsync(_ => true);
        }

        public async Task<List<Construction>> GetConstructionByUserId(string userId)
        {
            return await _constructionCollection
                .Find(x => x.UserId == userId)
                .ToListAsync();
        }
    }
}