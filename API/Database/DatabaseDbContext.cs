using API.Models.NoteCategoryModels;
using API.Models.NoteImageModels;
using API.Models.NoteModels;
using API.Models.UserModels;
using Microsoft.EntityFrameworkCore;

namespace API.Database
{
    public class DatabaseDbContext : DbContext
    {
        public DatabaseDbContext(DbContextOptions<DatabaseDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<UserCredentials> Credentials => Set<UserCredentials>();
        public DbSet<Note> Notes => Set<Note>();
        public DbSet<NoteCategory> NoteCategories => Set<NoteCategory>();
        public DbSet<NoteImage> NoteImages => Set<NoteImage>();
    }
}
