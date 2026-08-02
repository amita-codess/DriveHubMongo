using DriveHubMongo.Model;
namespace DriveHubMongo.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmailAsync(string email);

        Task AddUserAsync(User user);

        Task<List<User>> GetAllUsersAsync();

        Task UpdateUserAsync(User user);
    }
}