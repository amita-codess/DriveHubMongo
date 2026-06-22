using DriveHubBackend.Model;

namespace DriveHubBackend.Repositories
{
    public interface IUserRepository 
    {
        Task<User?> GetUserByEmailAsync(string email);

        Task AddUserAsync(User user);

        Task<List<User>> GetAllUsersAsync();

        Task SaveChangesAsync();
    }
}
