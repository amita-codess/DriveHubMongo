using DriveHubMongo.Model;

namespace DriveHubMongo.Repositories
{
    public interface IRentalCarRepository
    {
        Task<List<RentalCar>> GetAllAsync();

        Task<RentalCar?> GetByIdAsync(string id);

        Task<List<RentalCar>> GetByUserIdAsync(string userId);

        Task CreateAsync(RentalCar rentalCar);

        Task UpdateAsync(string id, RentalCar rentalCar);

        Task DeleteAsync(string id);
    }
}
