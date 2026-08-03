using DriveHubMongo.Model;

namespace DriveHubMongo.Repositories
{
    public interface IAgricultureRepository
    {
        Task<List<Agriculture>> GetAllAsync();

        Task<Agriculture?> GetByIdAsync(string id);

        Task CreateAsync(Agriculture agriculture);

        Task UpdateAsync(string id, Agriculture agriculture);

        Task DeleteAsync(string id);
    }
}
