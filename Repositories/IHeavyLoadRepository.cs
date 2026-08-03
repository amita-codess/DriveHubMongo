using DriveHubMongo.Model;

namespace DriveHubMongo.Repositories
{
    public interface IHeavyLoadRepository
    {
        Task<List<HeavyLoad>> GetAllAsync();

        Task<HeavyLoad?> GetByIdAsync(string id);

        Task<List<HeavyLoad>> GetByUserIdAsync(string userId);

        Task CreateAsync(HeavyLoad heavyLoad);

        Task UpdateAsync(string id, HeavyLoad heavyLoad);

        Task DeleteAsync(string id);
    }
}
