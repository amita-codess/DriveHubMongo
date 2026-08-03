using DriveHubMongo.Model;
using MongoDB.Driver;

namespace DriveHubMongo.Repositories
{
    public class RentalCarRepository : IRentalCarRepository
    {
        private readonly IMongoCollection<RentalCar> _collection;


        public RentalCarRepository(IConfiguration configuration)
        {
            var client = new MongoClient(
                configuration["MongoDbSettings:ConnectionString"]
            );


            var database = client.GetDatabase(
                configuration["MongoDbSettings:DatabaseName"]
            );


            _collection = database.GetCollection<RentalCar>("RentalCars");
        }



        public async Task<List<RentalCar>> GetAllAsync()
        {
            return await _collection
                .Find(x => true)
                .ToListAsync();
        }



        public async Task<RentalCar?> GetByIdAsync(string id)
        {
            return await _collection
                .Find(x => x.Id == id)
                .FirstOrDefaultAsync();
        }



        public async Task<List<RentalCar>> GetByUserIdAsync(string userId)
        {
            return await _collection
                .Find(x => x.UserId == userId)
                .ToListAsync();
        }



        public async Task CreateAsync(RentalCar rentalCar)
        {
            await _collection.InsertOneAsync(rentalCar);
        }



        public async Task UpdateAsync(string id, RentalCar rentalCar)
        {
            await _collection.ReplaceOneAsync(
                x => x.Id == id,
                rentalCar
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
