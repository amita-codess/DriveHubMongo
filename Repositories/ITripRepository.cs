using DriveHubMongo.Model;

namespace DriveHubMongo.Repositories
{
    public interface ITripRepository
    {
        Task<List<Trip>> GetAllTripsAsync();
        Task<Trip?> GetTripByIdAsync(string id);
        Task AddTripAsync(Trip trip);
        Task UpdateTripAsync(string id, Trip trip);
        Task DeleteTripAsync(string id);

        Task<List<Trip>> GetTripsByUserId(string userId);
    }
}