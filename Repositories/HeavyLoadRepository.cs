using DriveHubMongo.Model;
using MongoDB.Driver;

namespace DriveHubMongo.Repositories
{
    public class HeavyLoadRepository : IHeavyLoadRepository
    {

        private readonly IMongoCollection<HeavyLoad> _heavyLoadCollection;


        public HeavyLoadRepository(IConfiguration configuration)
        {

            var client = new MongoClient(
                configuration["MongoDbSettings:ConnectionString"]
            );


            var database = client.GetDatabase(
                configuration["MongoDbSettings:DatabaseName"]
            );


            _heavyLoadCollection =
                database.GetCollection<HeavyLoad>("HeavyLoad");

        }




        public async Task<List<HeavyLoad>> GetAllAsync()
        {
            return await _heavyLoadCollection
                .Find(_ => true)
                .ToListAsync();
        }





        public async Task<HeavyLoad?> GetByIdAsync(string id)
        {

            return await _heavyLoadCollection
                .Find(x => x.Id == id)
                .FirstOrDefaultAsync();

        }





        public async Task<List<HeavyLoad>> GetByUserIdAsync(string userId)
        {

            return await _heavyLoadCollection
                .Find(x => x.UserId == userId)
                .ToListAsync();

        }





        public async Task CreateAsync(HeavyLoad heavyLoad)
        {

            await _heavyLoadCollection.InsertOneAsync(heavyLoad);

        }





        public async Task UpdateAsync(string id, HeavyLoad heavyLoad)
        {

            await _heavyLoadCollection.ReplaceOneAsync(

                x => x.Id == id,

                heavyLoad

            );

        }





        public async Task DeleteAsync(string id)
        {

            await _heavyLoadCollection.DeleteOneAsync(

                x => x.Id == id

            );

        }

    }
}
