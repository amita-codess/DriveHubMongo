using DriveHubMongo.Data;
using DriveHubMongo.Model;
using DriveHubMongo.Repositories;
using MongoDB.Driver;

namespace DriveHubMongo.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MongoDbContext _context;

        public UserRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .Find(u => u.Email == email)
                .FirstOrDefaultAsync();
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Users.InsertOneAsync(user);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users
                .Find(_ => true)
                .ToListAsync();
        }
    }
}