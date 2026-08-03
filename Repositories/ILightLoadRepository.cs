using DriveHubMongo.Model;

namespace DriveHubMongo.Repositories
{
    public interface ILightLoadRepository
    {
        Task<List<LightLoad>> GetAllAsync();
        Task<LightLoad?> GetByIdAsync(string id);
        Task<List<LightLoad>> GetByUserIdAsync(string userId);
        Task CreateAsync(LightLoad lightLoad);
        Task UpdateAsync(string id, LightLoad lightLoad);
        Task DeleteAsync(string id);
    }
}
