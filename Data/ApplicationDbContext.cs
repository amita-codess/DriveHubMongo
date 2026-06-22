using DriveHubBackend.Model;
using Microsoft.EntityFrameworkCore;

namespace DriveHubBackend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
           DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        public DbSet<User> Users
        {
            get;
            set;
        }
    }
}
