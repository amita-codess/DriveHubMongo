using DriveHubMongo.Model;

namespace DriveHubMongo.Repositories
{
    public interface IConstructionRepository
    {
        Task<List<Construction>> GetAllConstructionAsync();

        Task<Construction?> GetConstructionByIdAsync(string id);

        Task AddConstructionAsync(Construction construction);

        Task UpdateConstructionAsync(string id, Construction construction);

        Task DeleteConstructionAsync(string id);

        Task DeleteAllConstructionAsync();

        Task<List<Construction>> GetConstructionByUserId(string userId);
    }
}