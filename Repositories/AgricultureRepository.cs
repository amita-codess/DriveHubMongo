using DriveHubMongo.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DriveHubMongo.Repositories
{
    public class AgricultureRepository : IAgricultureRepository
    {
        private readonly IMongoCollection<Agriculture> _agricultureCollection;

        public AgricultureRepository(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);

            _agricultureCollection =
                database.GetCollection<Agriculture>("Agriculture");
        }

        public async Task<List<Agriculture>> GetAllAsync()
        {
            return await _agricultureCollection
                .Find(_ => true)
                .ToListAsync();
        }

        public async Task<Agriculture?> GetByIdAsync(string id)
        {
            return await _agricultureCollection
                .Find(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Agriculture agriculture)
        {
            await _agricultureCollection.InsertOneAsync(agriculture);
        }

        public async Task UpdateAsync(string id, Agriculture agriculture)
        {
            agriculture.Id = id;

            await _agricultureCollection.ReplaceOneAsync(
                x => x.Id == id,
                agriculture
            );
        }

        public async Task DeleteAsync(string id)
        {
            await _agricultureCollection.DeleteOneAsync(
                x => x.Id == id
            );
        }
    }
}
