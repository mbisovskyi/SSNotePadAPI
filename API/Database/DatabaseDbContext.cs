using API.Models.UserModels;
using Microsoft.EntityFrameworkCore;

namespace API.Database
{
    public class DatabaseDbContext : DbContext
    {
        public DatabaseDbContext(DbContextOptions<DatabaseDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<UserCredentials> Credentials => Set<UserCredentials>();
    }
}
