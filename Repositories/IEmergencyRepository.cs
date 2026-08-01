using DriveHubMongo.Model;

namespace DriveHubMongo.Repositories
{
    public interface IEmergencyRepository
    {
        Task<List<Emergency>> GetAllEmergencyAsync();

        Task<Emergency?> GetEmergencyByIdAsync(string id);

        Task AddEmergencyAsync(Emergency emergency);

        Task UpdateEmergencyAsync(string id, Emergency emergency);

        Task DeleteEmergencyAsync(string id);

        Task DeleteAllEmergencyAsync();

        Task<List<Emergency>> GetEmergencyByUserId(string userId);
    }
}